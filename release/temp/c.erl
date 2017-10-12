-module(c).

-export([
  checksum/1
]).

checksum(Bin) ->
  checksum(Bin, 0).
checksum(<<>>, ACC) ->
  ACC;
checksum(<<X:8, Rest/binary>>, ACC) ->
  R = ACC bxor X,
  checksum(Rest, R).