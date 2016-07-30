#include "Mock_Logger.h"
#include "..\CPlusPlusBusiness\Person.h"
#include "..\CPlusPlusBusiness\Person.cpp"
#include "gtest\gtest.h"
#include <string>

using ::testing::AtLeast;

//Testing Person class using MockLogger dependecies with set expectataions


TEST(PersonClassTest, PrintNamefunctionTest) {
	MockLogger mockLogger;
	std::string str = "Nishit";

	//Log message should be called at least once with given argument str
	EXPECT_CALL(mockLogger, LogMessage(str)).Times(AtLeast(1)); 

	
	Person  person(&mockLogger, str,19);
	person.PrintName();

	//Log message should be called at least twice with given argument str
	EXPECT_CALL(mockLogger, LogMessage(str)).Times(AtLeast(2));
	person.PrintName();
	person.PrintName();
}

TEST(PersonClassTest, PrintInfofunctionTest) {
	MockLogger mockLogger;
	std::string str = "Nishit19";
	EXPECT_CALL(mockLogger, LogMessage(str)).Times(AtLeast(1));

	str = "Nishit";
	Person  person(&mockLogger, str, 19);
	person.PrintInfo();

	
}

