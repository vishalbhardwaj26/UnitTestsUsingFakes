#pragma once

#include <string>
class Logger
{
public:
	Logger()
	{}
	virtual ~Logger(){}

	virtual void LogMessage(std::string message) 
	{}
	virtual void LogInteger(int nMessage)
	{}
	virtual int LogFileSize()
	{
		return 0;
	}

private:

};

