%%%-------------------------------------------------------------------
%%% @author xugg
%%% @copyright (C) 2017, <COMPANY>
%%% @doc
%%%
%%% @end
%%% Created : 23. Aug 2017 11:03
%%%-------------------------------------------------------------------
-module(eze_client_t).
-author("xugg").

-behavior(shackle_client).

-include("eze.hrl").

%% API
-export([
  start/1,
  heartbeat/1,
  position/1
]).

-export([
  init/0,
  setup/2,
  handle_request/2,
  handle_data/2,
  terminate/1
]).

-record(state, {
  buffer =       <<>> :: binary(),
  request_counter = 0 :: non_neg_integer()
}).

start(PoolName) ->
  start(?POOL_SIZE, PoolName).

%%-spec start(pos_integer()) ->
%%  ok | {error, shackle_not_started | pool_already_started}.
start(PoolSize, PoolName) ->
  {ok, IP} = server_ip(),
  {ok, Port} = server_port(),

%%  lager:info("===~p===: ~p (~p) ->  Getting app configuration, IP = ~p, Port = ~p ~n",
%%    [?MODULE, ?LINE, self(), IP, Port]),
  shackle_pool:start(PoolName, ?CLIENT_TCP, [
    {ip, IP},
    {port, Port},
    {reconnect, true},
    {socket_options, [
      binary,
      {packet, raw}
    ]}
  ], [
    {backlog_size, ?BACKLOG_SIZE},
    {pool_size, PoolSize}
  ]).

heartbeat(Pool) ->
  shackle:call(Pool, {heartbeat, Pool}, ?TIMEOUT).

position(Pool) ->
  shackle:call(Pool, {position, Pool}, ?TIMEOUT).

init() ->
  {ok, #state {}}.

setup(Socket, State) ->
%%  Reg = "7E0100002D9161227001100001002C012C373035303348543631314130303030303030303030303030300207000001010001D4C1423132333435A77E",
%%  RegBin = hex:hexstr_to_bin(Reg),
%%  RegRtn = "7e8100000e9161227001100001000100313233343536373839305a677e",
%%  RegRtnBin = hex:hexstr_to_bin(RegRtn),
%%  case gen_tcp:send(Socket, <<RegBin/binary>>) of
%%    ok ->
%%      case gen_tcp:recv(Socket, 0) of
%%        {ok, <<RegRtnBin/binary>>} ->
%%          {ok, State};
%%        {error, Reason} ->
%%          {error, Reason, State}
%%      end;
%%    {error, Reason} ->
%%      {error, Reason, State}
%%  end.
  {ok, State}.

handle_request({Operation, Pool}, #state {
  request_counter = RequestCounter
} = State) ->
  SeqNo = seq_no(RequestCounter),
  Mobile = list_to_integer(atom_to_list(Pool)) + 16#010000000000,%%mobile(),
  RequestId = arithmetic_protocol:request_id(RequestCounter),
  Data = arithmetic_protocol:request(RequestId, Operation, SeqNo, Mobile),
%%  lager:info("===~p===: ~p(~p) -> Sending  : ~p ~n",
%%    [?MODULE, ?LINE, self(), hex:bin_to_hexstr(Data)]),
  eze_counter:add(),
  {ok, RequestId, Data, State#state {
    request_counter = RequestCounter + 1
  }}.

seq_no(Counter) ->
%%  case Counter of
%%    C when C < 16#FFFF -> C;
%%
%%  end
  mod(Counter, 16#FFFF).

mobile() ->
  1.

mod(X,Y) when X > 0 -> X rem Y;
mod(X,Y) when X < 0 -> Y + X rem Y;
mod(0,_) -> 0.

handle_data(Data, #state {
  buffer = Buffer
} = State) ->

  Data2 = <<Buffer/binary, Data/binary>>,
  %{Replies, Buffer2} = arithmetic_protocol:parse_replies(Data2, []),
  Replies = [],

  {ok, Replies, State#state {
    buffer = Buffer
  }}.

-spec terminate(State :: term()) -> ok.
terminate(_State) -> ok.

server_ip() ->
  application:get_env(eze_simulator_th,ip).

server_port() ->
  application:get_env(eze_simulator_th,port).


