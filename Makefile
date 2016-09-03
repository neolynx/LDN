#!/bin/sh

SLN:=LDN

all: restore
	#xbuild /property:ReferencePath=/asdqwe;OutputFile=../../bla
	xbuild '/p:OutputPath=../../out/$$(RootNamespace)/'

restore:
	nuget restore $(SLN).sln

clean:
	xbuild /t:Clean
	rm -rf out

install:
	mkdir -p $(DESTDIR)/opt/$(SLN)/
	cp -r out/* $(DESTDIR)/opt/$(SLN)/

