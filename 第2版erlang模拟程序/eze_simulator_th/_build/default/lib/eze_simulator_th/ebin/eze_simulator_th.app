{application, eze_simulator_th,
 [{description, "An 808 simulator application"},
  {vsn, "0.1.0"},
  {registered, []},
  {mod, {eze_simulator_th_app, []}},
  {applications,
   [kernel,
    stdlib,
    syntax_tools,
    compiler,
    goldrush,
    lager,
    granderl,
    metal,
    shackle
   ]},
  {env,[{num, 50},
   {mul, 10},
   {sta, 1000000},
   {ip, "127.0.0.1"},
   {port, 10002}
   ]},
  {modules, []},

  {maintainers, []},
  {licenses, ["Apache 2.0"]},
  {links, []}
 ]}.
