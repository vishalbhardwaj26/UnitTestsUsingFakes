#pragma once
#include<string>
#include "Logger.h"

///Person class using logger to log information underneath
class Person
{
public:
	Person();
	Person(Logger* ILogger, std::string strName, int nAge);
	~Person();
	void PrintInfo();
	void PrintName();
	void PrintAge();

private:
	Logger* _logger;
	std::string _strName;
	int _nAge;

};

