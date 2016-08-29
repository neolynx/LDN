#!/bin/sh

SLN:=LDN

all: restore
	xbuild

restore:
	nuget restore $(SLN).sln

clean:
	xbuild /t:Clean
