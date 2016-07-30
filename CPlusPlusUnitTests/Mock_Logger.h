#pragma once
#include "stdafx.h"
#include "..\CPlusPlusBusiness\Logger.h"
#include "gmock\gmock.h"
#include <string>


///Create Mock logger dependencies to be used with person class to test
class MockLogger : public Logger
{
public:
	MockLogger() :Logger()
	{}
	MOCK_METHOD1(LogMessage, void(std::string message));
	MOCK_METHOD1(LogInteger, void(int message));
	
	~MockLogger()
	{

	}


};


