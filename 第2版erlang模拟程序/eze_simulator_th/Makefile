PROJECT = eze_simulator_th
PROJECT_DESCRIPTION = TCP 808 Client Simulator
PROJECT_VERSION = 0.1

DEPS = gen_logger
dep_gen_logger = git https://github.com/emqtt/gen_logger.git

DEPS = shackle
dep_shackle = git https://github.com/lpgauth/shackle.git


EUNIT_OPTS = verbose

CT_SUITES = tr

ERLC_OPTS += +debug_info
ERLC_OPTS += +'{parse_transform, lager_transform}'

COVER = true

include erlang.mk

app:: rebar.config

.PHONY: all test clean

##clean:
##	rm -rf ebin/*.beam

compile: clean
	mkdir ebin
	cp src/eze_simulator_th.app.src ebin/eze_simulator_th.app
	erlc -pa deps/goldrush/ebin/\
		-pa deps/shackle/_build/compile/lib/granderl/ebin/\
		-pa deps/lager/ebin/\
		-pa deps/shackle/_build/compile/lib/metal/ebin/\
		-pa deps/shackle/_build/compile/lib/shackle/ebin/\
		-o ebin/\
		$(ERLC_OPTS) \
		src/*.erl
	erlc -o ebin test/*.erl

run:
	erl -pa deps/goldrush/ebin/\
		-pa deps/shackle/_build/compile/lib/granderl/ebin/\
		-pa deps/lager/ebin/\
		-pa deps/shackle/_build/compile/lib/metal/ebin/\
		-pa deps/shackle/_build/compile/lib/shackle/ebin/\
		-pa ebin/\
		-config sys.config \
	-eval 'application:ensure_all_started(eze_simulator_th).'



test:
	erl -noshell -pa ebin\
    -eval 'eunit:test("ebin",[verbose])' \
    -s init stop

go: compile run

