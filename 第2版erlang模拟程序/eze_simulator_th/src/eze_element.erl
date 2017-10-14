%%%-------------------------------------------------------------------
%%% @author xugg
%%% @copyright (C) 2017, <COMPANY>
%%% @doc
%%%
%%% @end
%%% Created : 24. Aug 2017 08:58
%%%-------------------------------------------------------------------
-module(eze_element).
-author("xugg").

-behaviour(gen_server).

%% API
-export([start_link/2,
  create/2,
  callback/2,
  get_interval/1,
  wait/1
  ]).

%% gen_server callbacks
-export([init/1,
  handle_call/3,
  handle_cast/2,
  handle_info/2,
  terminate/2,
  code_change/3]).

-define(SERVER, ?MODULE).

-record(state, {type, pool}).

%%%===================================================================
%%% API
%%%===================================================================

%%--------------------------------------------------------------------
%% @doc
%% Starts the server
%%
%% @end
%%--------------------------------------------------------------------
start_link(Type, Pool) ->
  gen_server:start_link(?MODULE, [Type, Pool], []).

create(Type, Pool) ->
  eze_element_sup:start_child(Type, Pool).

callback(Type, Pool) ->
  case Type of
    heartbeat -> eze_client_t:heartbeat(Pool);
    position -> eze_client_t:position(Pool);
    _ -> ok
  end,
  ok.

get_interval(Type) ->
  case Type of
    heartbeat -> {2, minute};
    position -> {15, second}
  end.

%%%===================================================================
%%% gen_server callbacks
%%%===================================================================

%%--------------------------------------------------------------------
%% @private
%% @doc
%% Initializes the server
%%
%% @spec init(Args) -> {ok, State} |
%%                     {ok, State, Timeout} |
%%                     ignore |
%%                     {stop, Reason}
%% @end
%%--------------------------------------------------------------------
-spec(init(Args :: term()) ->
  {ok, State :: #state{}} | {ok, State :: #state{}, timeout() | hibernate} |
  {stop, Reason :: term()} | ignore).
init([Type, Pool]) ->
%%  lager:info("===~p===: ~p (~p) -> look at me! Type = ~p, Pool = ~p ~n",
%%    [?MODULE, ?LINE, self(), Type, Pool]),
  case Type of
    position ->
      eze_counter:padd();
    _ -> ok
  end,

  %%{ok, Pid} = gen_interval:start_link(self()),
  interval(self(), Type, Pool),
  {ok, #state{type=Type, pool = Pool}}.

%%--------------------------------------------------------------------
%% @private
%% @doc
%% Handling call messages
%%
%% @end
%%--------------------------------------------------------------------
-spec(handle_call(Request :: term(), From :: {pid(), Tag :: term()},
    State :: #state{}) ->
  {reply, Reply :: term(), NewState :: #state{}} |
  {reply, Reply :: term(), NewState :: #state{}, timeout() | hibernate} |
  {noreply, NewState :: #state{}} |
  {noreply, NewState :: #state{}, timeout() | hibernate} |
  {stop, Reason :: term(), Reply :: term(), NewState :: #state{}} |
  {stop, Reason :: term(), NewState :: #state{}}).
handle_call(_Request, _From, State) ->
  #state{type=Type, pool = Pool} = State,
  interval(self(), Type, Pool),
  {reply, ok, State}.

%%--------------------------------------------------------------------
%% @private
%% @doc
%% Handling cast messages
%%
%% @end
%%--------------------------------------------------------------------
-spec(handle_cast(Request :: term(), State :: #state{}) ->
  {noreply, NewState :: #state{}} |
  {noreply, NewState :: #state{}, timeout() | hibernate} |
  {stop, Reason :: term(), NewState :: #state{}}).
handle_cast(_Request, State) ->
  {noreply, State}.

%%--------------------------------------------------------------------
%% @private
%% @doc
%% Handling all non call/cast messages
%%
%% @spec handle_info(Info, State) -> {noreply, State} |
%%                                   {noreply, State, Timeout} |
%%                                   {stop, Reason, State}
%% @end
%%--------------------------------------------------------------------
-spec(handle_info(Info :: timeout() | term(), State :: #state{}) ->
  {noreply, NewState :: #state{}} |
  {noreply, NewState :: #state{}, timeout() | hibernate} |
  {stop, Reason :: term(), NewState :: #state{}}).
handle_info(_Info, State) ->
  eze_counter:pdel(),
  {noreply, State}.

%%--------------------------------------------------------------------
%% @private
%% @doc
%% This function is called by a gen_server when it is about to
%% terminate. It should be the opposite of Module:init/1 and do any
%% necessary cleaning up. When it returns, the gen_server terminates
%% with Reason. The return value is ignored.
%%
%% @spec terminate(Reason, State) -> void()
%% @end
%%--------------------------------------------------------------------
-spec(terminate(Reason :: (normal | shutdown | {shutdown, term()} | term()),
    State :: #state{}) -> term()).
terminate(_Reason, _State) ->
  ok.

%%--------------------------------------------------------------------
%% @private
%% @doc
%% Convert process state when code is changed
%%
%% @spec code_change(OldVsn, State, Extra) -> {ok, NewState}
%% @end
%%--------------------------------------------------------------------
-spec(code_change(OldVsn :: term() | {down, term()}, State :: #state{},
    Extra :: term()) ->
  {ok, NewState :: #state{}} | {error, Reason :: term()}).
code_change(_OldVsn, State, _Extra) ->
  {ok, State}.

%%%===================================================================
%%% Internal functions
%%%===================================================================

interval(Pid, Type, Pool) ->
  ok		= callback(Type, Pool),%%gen_server:call(CBPid, {callback}),%%CBPid:callback(),
  Interval	= get_interval(Type),%%gen_server:call(CBPid, {get_interval}),%%CBPid:get_interval(),
  IntervalMS  = time_interval:get_interval(Interval),
%%  io:format("===~p===: ~p (~p) -> Interval ~p~n",
%%    [?MODULE, ?LINE, self(), IntervalMS]),
  {ok, _TRef} = timer:apply_after(IntervalMS, ?MODULE, wait, [Pid]),

  ok.

wait(Pid) ->
  gen_server:call(Pid, {interval}).