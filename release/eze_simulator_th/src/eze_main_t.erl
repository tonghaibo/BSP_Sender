%%%-------------------------------------------------------------------
%%% @author xugg
%%% @copyright (C) 2017, <COMPANY>
%%% @doc
%%%
%%% @end
%%% Created : 23. Aug 2017 16:03
%%%-------------------------------------------------------------------
-module(eze_main_t).
-author("xugg").

-include("eze.hrl").

%% API
-export([start/0,
  run/0,
  run/2
  ]).

start() ->
  %%{ok, Mul} = application:get_env(eze_simulator_th,mul),
  %%start(Mul),
  ok.

%%start(0) ->
%%  ok;
%%start(M) ->
%%  PoolName = pool_name(M),
%%  lager:info("===~p===: ~p(~p) ->  Create pool : ~p ~n",
%%    [?MODULE, ?LINE, self(), PoolName]),
%%  ok = eze_client_t:start(PoolName),
%%  start(M - 1).

run() ->
  {ok, Num} = application:get_env(eze_simulator_th,num),
  {ok, Mul} = application:get_env(eze_simulator_th,mul),
  lager:info("===~p===: ~p(~p) ->  Getting app configuration, Num = ~p, Mul = ~p ~n",
    [?MODULE, ?LINE, self(), Num, Mul]),

  run_m(Num, Mul),
  {ok, self()}.

run_m(_Num, 0) ->
  ok;
run_m(Num, M) ->
  PoolName = pool_name(M),
  start_pool(PoolName),
  run(Num, PoolName),
  run_m(Num, M -1).

run(0, _Pool) ->
  ok;
run(undefined, _) ->
  ok;
run(Num, Pool) ->
  eze_element:create(heartbeat, Pool),
  eze_element:create(position, Pool),
  run(Num - 1, Pool).

pool_name(M) ->
  Prev = ?POOL_NAME,
  PoolName = atom_to_list(Prev) ++ integer_to_list(M),
  list_to_atom(PoolName).

start_pool(PoolName) ->
  lager:info("===~p===: ~p(~p) ->  Create pool : ~p ~n",
    [?MODULE, ?LINE, self(), PoolName]),
  ok = eze_client_t:start(PoolName).
