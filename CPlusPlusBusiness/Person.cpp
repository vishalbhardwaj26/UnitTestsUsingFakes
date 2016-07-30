#include "stdafx.h"
#include "Person.h"

void Person::PrintInfo()
{
	//Business logic
	//
	std::string str = _strName + std::to_string(_nAge);
	_logger->LogMessage(str);
}
void Person::PrintName()
{	
	//Business logic
	//
	_logger->LogMessage(_strName);
}
void Person::PrintAge()
{
	//Business logic
	//
	_logger->LogInteger(_nAge);
}
Person::Person()
{
}

Person::Person(Logger* ILogger, std::string strName, int nAge)
{
	_strName = strName;
	_nAge = nAge;
	_logger = ILogger;
}

Person::~Person()
{
	//delete _logger;
}

