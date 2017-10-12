%%%-------------------------------------------------------------------
%%% @author xugg
%%% @copyright (C) 2017, <COMPANY>
%%% @doc
%%%
%%% @end
%%% Created : 23. Aug 2017 11:29
%%%-------------------------------------------------------------------
-module(arithmetic_protocol).

-export([
  opcode/1,
  parse_replies/1,
  parse_requests/1,
  request/3,
  request_id/1,
  get_heartbeat_bin/1
]).

-define(MAX_REQUEST_ID, 4294967296).

-type int() :: 9..4294967295.
-type operation() :: add | multiply.
-type tiny_int() :: 0..255.

%% public
-spec opcode(operation()) -> 1..2.

opcode(add) -> 1;
opcode(multiply) -> 2.

parse_replies(Data) ->
  parse_replies(Data, []).

-spec parse_requests(binary()) -> {[binary()], binary()}.

parse_requests(Data) ->
  parse_requests(Data, []).

-spec request(int(), operation(), int()) -> binary().

request(ReqId, Operation, RequestCounter) ->

  %%Heartbeat = "7E000200009161227001100004B57E",
%%  Position = "7E020000229161227001100003
%%  00000100004400000000000000000000000000000000040101002447010400002960FE7E",
  %%HeartbeatBin = hex:hexstr_to_bin(Heartbeat),
  %% PositionBin = hex:hexstr_to_bin(Position),
  case Operation of
    heartbeat ->
      HeartbeatBin = get_heartbeat_bin(RequestCounter),
      <<HeartbeatBin/binary>>;
    position ->
      PositionBin = get_position_bin(RequestCounter),
      <<PositionBin/binary>>;
    _ -> <<>>
  end.
  %%<<ReqId:32/integer, (opcode(Operation)), A:8/integer, B:8/integer>>.

get_heartbeat_bin(SeqNo) ->
  Tag = "7E",
  MsgId = "0002",
  MsgProp = "0000",
  Mobile = "916122700110",
  %%SeqNo = "0004",
  Body = "",
  %%CheckSum = "B5",
  get_bin(Tag, MsgId, MsgProp, Mobile, SeqNo, Body).

get_position_bin(SeqNo) ->
  Tag = "7E",
  MsgId = "0200",
  MsgProp = "0022",
  Mobile = "916122700110",
  %%SeqNo = "0004",
  Body = "00000100004400000000000000000000000000000000040101002447010400002960",
  %%CheckSum = "B5",
  get_bin(Tag, MsgId, MsgProp, Mobile, SeqNo, Body).

get_bin(Tag, MsgId, MsgProp, Mobile, SeqNo, Body) ->
  TagBin = hex:hexstr_to_bin(Tag),
  SeqNoBin = <<(SeqNo+1):16>>,
  SeqNo1 = hex:bin_to_hexstr(SeqNoBin),
  H1 = MsgId ++ MsgProp ++ Mobile ++ SeqNo1 ++ Body, %%++ CheckSum ++ Tag,
  HH1 = hex:hexstr_to_bin(H1),
  CheckSum1 = checksum(HH1),
  CheckSumBin = binary:encode_unsigned(CheckSum1),
  CHex = hex:bin_to_hexstr(CheckSumBin),
  H2 = H1 ++ CHex,
  HH2 = hex:hexstr_to_bin(H2),
  %%ChexBin =hex:hexstr_to_bin(CHex).
  %%<<CheckSumBin:8>>.
  %%CheckSumBin.
  HH3 = encode_7e_7d(HH2),
  <<TagBin/binary, HH3/binary, TagBin/binary>>.%%, TagBin/binary>>.

-spec request_id(non_neg_integer()) -> tiny_int().
request_id(RequestCounter) ->
  RequestCounter rem ?MAX_REQUEST_ID.

%% private
parse_replies(<<ReqId:32/integer, A:16/integer, Rest/binary>>, Acc) ->
  parse_replies(Rest, [{ReqId, A} | Acc]);
parse_replies(Buffer, Acc) ->
  {Acc, Buffer}.

parse_requests(<<"INIT", Rest/binary>>, Acc) ->
  parse_requests(Rest, [<<"OK">> | Acc]);
parse_requests(<<ReqId:32/integer, 1, A:8/integer, B:8/integer,
  Rest/binary>>, Acc) ->

  parse_requests(Rest, [<<ReqId:32/integer, (A + B):16/integer>> | Acc]);
parse_requests(<<ReqId:32/integer, 2, A:8/integer, B:8/integer,
  Rest/binary>>, Acc) ->

  parse_requests(Rest, [<<ReqId:32/integer, (A * B):16/integer>> | Acc]);
parse_requests(Buffer, Acc) ->
  {Acc, Buffer}.

checksum(Bin) ->
  checksum(Bin, 0).
checksum(<<>>, ACC) ->
  ACC;
checksum(<<X:8, Rest/binary>>, ACC) ->
  R = ACC bxor X,
  checksum(Rest, R).

encode_7e_7d(Bin) ->
  encode_7e_7d(Bin, <<>>).
encode_7e_7d(<<>>, ACC) ->
  ACC;
encode_7e_7d(<<X:8, Rest/binary>>, ACC) ->
  Y =
    case X of
      16#7e -> 16#7d02;
      16#7d -> 16#7d01;
      _-> X
    end,
  BY = binary:encode_unsigned(Y),
  R = <<ACC/binary, BY/binary>>,
  encode_7e_7d(Rest, R).

decode_7e_7d(Bin) ->
  decode_7e_7d(Bin, <<>>).
%%decode_7e_7d(<<>>, ACC) ->
%%  ACC;
decode_7e_7d(<<X:8>>, ACC) ->
  BX = binary:encode_unsigned(X),
  <<ACC/binary, BX/binary>>;
decode_7e_7d(<<X:8, Rest/binary>>, ACC) ->
  <<Y:8, RRest/binary>> = Rest,
  {Z, RRRest} =
    case X of
      16#7d ->
        case Y of
          16#01 -> {16#7d, RRest};
          16#02 -> {16#7e, RRest};
          _ -> {Y, Rest}
        end;
      _ -> {X, Rest}
    end,
  BZ = binary:encode_unsigned(Z),
  decode_7e_7d(RRRest, <<ACC/binary, BZ/binary>>).
