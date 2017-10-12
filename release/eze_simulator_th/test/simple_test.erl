%%%-------------------------------------------------------------------
%%% @author xugg
%%% @copyright (C) 2017, <COMPANY>
%%% @doc
%%%
%%% @end
%%% Created : 22. Aug 2017 11:21
%%%-------------------------------------------------------------------
-module(simple_test).
-author("xugg").

-include_lib("eunit/include/eunit.hrl").

simple_test() ->
  ?assert(true).

eze_t_test() ->
  ok = eze_client_t:test().

eze_t_2_test() ->
  ok.
%%  error_logger:tty(false),
%%  shackle_app:start(),
%%  ok = eze_client_t:start(),
  %%eze_client_t:add(1,2).

eze_bin_test() ->
  Heartbeat = "7E000200009161227001100004B57E",
  Position = "7E02000022916122700110000300000100004400000000000000000000000000000000040101002447010400002960FE7E",
  HeartbeatBin = hex:hexstr_to_bin(Heartbeat),
  PositionBin = hex:hexstr_to_bin(Position),
  HB = arithmetic_protocol:get_heartbeat_bin(3),
  ?assertEqual(HeartbeatBin, HB).
