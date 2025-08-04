#!/usr/bin/env bash
export NIXPKGS_ALLOW_UNFREE=1
tmp="/tmp/ypm-dev"

mkdir $tmp
nix-shell --run "TMPDIR=$tmp code ."
