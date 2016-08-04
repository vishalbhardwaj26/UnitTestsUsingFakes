#include "Mock_Logger.h"
#include "..\CPlusPlusBusiness\Person.h"
#include "..\CPlusPlusBusiness\Person.cpp"
#include "gtest\gtest.h"
#include <string>

using ::testing::AtLeast;
using ::testing::Return;
using ::testing::InSequence;


//Testing Person class using MockLogger dependecies with set expectataions


TEST(PersonClassTest, PrintNamefunctionTest) {
	MockLogger mockLogger;
	std::string str = "Nishit";

	//Log message should be called at least once with given argument str
	EXPECT_CALL(mockLogger, LogMessage(str)).Times(AtLeast(1)); 

	
	Person  person(&mockLogger, "Nishit",19);
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

TEST(PersonClassTest, LogFileSizefunctionTest) {
	MockLogger mockLogger;
	std::string str = "Nishit19";

	InSequence dummy;
	//Log message should be called at least once with given argument str
	EXPECT_CALL(mockLogger, LogFileSize()).Times(AtLeast(1)).WillOnce(Return(200));
	EXPECT_CALL(mockLogger, LogMessage(str)).Times(AtLeast(1));
	
	str = "Nishit";
	Person  person(&mockLogger, str, 19);
	person.PrintInfo();
	
	
}

//"$(TargetDir)$(TargetFileName)" 

