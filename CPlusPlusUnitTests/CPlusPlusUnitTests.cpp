// CPlusPlusUnitTests.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <gtest\gtest.h>


int main(int ac, char* av[])
{
	testing::InitGoogleTest(&ac, av);
	return RUN_ALL_TESTS();
}

