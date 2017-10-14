%%%-------------------------------------------------------------------
%% @doc eze_simulator public API
%% @end
%%%-------------------------------------------------------------------

-module(eze_simulator_th_app).

-behaviour(application).

%% Application callbacks
-export([start/2, stop/1]).

%%====================================================================
%% API
%%====================================================================

start(_StartType, _StartArgs) ->
    lager:start(),
    shackle_app:start(),

    eze_simulator_th_sup:start_link(),
    %%application:ensure_all_started(eze_simulator_th),

    eze_main_t:run().
%%    eze_client_t:test().

%%--------------------------------------------------------------------
stop(_State) ->
    ok.

%%====================================================================
%% Internal functions
%%====================================================================
