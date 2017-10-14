werl -pa deps/goldrush/ebin/^
    -pa deps/shackle/_build/default/lib/granderl/ebin/^
    -pa deps/lager/ebin/^
    -pa deps/shackle/_build/default/lib/metal/ebin/^
    -pa deps/shackle/_build/compile/lib/shackle/ebin/^
    -pa ebin/^
    -config sys.config ^
    -smp true +K true +A 32 +P 1000000 ^
    -env ERL_MAX_PORTS 500000 -env ERTS_MAX_PORTS 500000 ^
    -eval "application:ensure_all_started(eze_simulator_th)."

