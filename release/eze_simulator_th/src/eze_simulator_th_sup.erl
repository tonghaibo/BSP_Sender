%%%-------------------------------------------------------------------
%% @doc eze_simulator top level supervisor.
%% @end
%%%-------------------------------------------------------------------

-module(eze_simulator_th_sup).

-behaviour(supervisor).

%% API
-export([start_link/0]).

%% Supervisor callbacks
-export([init/1]).

-define(SERVER, ?MODULE).

%%====================================================================
%% API functions
%%====================================================================

start_link() ->
    supervisor:start_link({local, ?SERVER}, ?MODULE, []).

%%====================================================================
%% Supervisor callbacks
%%====================================================================

%% Child :: {Id,StartFunc,Restart,Shutdown,Type,Modules}
init([]) ->
    RestartStrategy = one_for_one,
    MaxRestarts = 4, %%1000, %%0,
    MaxSecondsBetweenRestarts = 3600, %%3600, %%1,
    SupFlags = {RestartStrategy, MaxRestarts, MaxSecondsBetweenRestarts},

    %% tr_server
%%  Restart = permanent,
%%  Shutdown = 2000,
%%  Type = worker,
%%  AChild = {'tr_server', {'tr_server', start_link, []},
%%    Restart, Shutdown, Type, ['tr_server']},

    %% element sup
    ElementSup = {eze_element_sup, {eze_element_sup, start_link, []},
        permanent, 2000, supervisor, [eze_element]},

    {ok, {SupFlags, [ElementSup]}}.

%%====================================================================
%% Internal functions
%%====================================================================
