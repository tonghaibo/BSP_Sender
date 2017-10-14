%%%-------------------------------------------------------------------
%%% @author xugg
%%% @copyright (C) 2017, <COMPANY>
%%% @doc
%%%
%%% @end
%%% Created : 12. Oct 2017 16:36
%%%-------------------------------------------------------------------
-module(eze_counter).
-author("xugg").

-behaviour(gen_server).

%% API
-export([start_link/0,
  add/0,
  st/0,
  num/0,
  speed/0,
  padd/0,
  pdel/0,
  pnum/0]).

%% gen_server callbacks
-export([init/1,
  handle_call/3,
  handle_cast/2,
  handle_info/2,
  terminate/2,
  code_change/3]).

-define(SERVER, ?MODULE).

-record(state, {num, st, pnum}).

%%%===================================================================
%%% API
%%%===================================================================

%%--------------------------------------------------------------------
%% @doc
%% Starts the server
%%
%% @end
%%--------------------------------------------------------------------
-spec(start_link() ->
  {ok, Pid :: pid()} | ignore | {error, Reason :: term()}).
start_link() ->
  gen_server:start_link({local, ?SERVER}, ?MODULE, [], []).

add() ->
  gen_server:cast(?SERVER, add).

st() ->
  gen_server:call(?SERVER, st).

num() ->
  gen_server:call(?SERVER, num).

speed() ->
  gen_server:call(?SERVER, speed).

padd() ->
  gen_server:cast(?SERVER, padd).

pdel() ->
  gen_server:cast(?SERVER, pdel).

pnum() ->
  gen_server:call(?SERVER, pnum).


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
init([]) ->
  Now = calendar:local_time(),
  {ok, #state{st = Now, num = 0, pnum = 0}}.

%%--------------------------------------------------------------------
%% @private
%% @doc
%% Handling call messages
%%
%% @end
%%--------------------------------------------------------------------
handle_call(st, _From, State=#state{st = ST}) ->
  {reply, {ok, ST}, State};
handle_call(num, _From, State=#state{num = Num}) ->
  {reply, {ok, Num}, State};
handle_call(speed, _From, State=#state{st = ST, num = Num}) ->
  STSeconds = calendar:datetime_to_gregorian_seconds(ST),
  Now = calendar:local_time(),
  NowSeconds = calendar:datetime_to_gregorian_seconds(Now),
  {reply, {ok, Num/(NowSeconds - STSeconds)}, State};
handle_call(pnum, _From, State=#state{pnum = PNum}) ->
  {reply, {ok, PNum}, State};
handle_call(_Request, _From, State) ->
  {reply, ok, State}.

%%--------------------------------------------------------------------
%% @private
%% @doc
%% Handling cast messages
%%
%% @end
%%--------------------------------------------------------------------
handle_cast(add, State=#state{num = Num}) ->
  {noreply, State#state{num=Num+1}};
handle_cast(padd, State=#state{pnum = PNum}) ->
  {noreply, State#state{pnum=PNum+1}};
handle_cast(pdel, State=#state{pnum = PNum}) ->
  {noreply, State#state{pnum=PNum-1}};
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
