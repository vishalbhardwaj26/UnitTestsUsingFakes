#include "stdafx.h"
#include <gtest\gtest.h>
#include <iostream>


#include "..\CPlusPlusBusiness\Queue.h"

class QueueClassTest : public ::testing::Test {

protected:

	virtual void SetUp() {

		printf("%s", "SettinUpTest");

	}
	virtual void TearDown() {
		// Code here will be called immediately after each test
		// (right before the destructor).
		printf("%s", "******************DestructingTest");
	}

};

TEST(FrameworkTest, test_1)
{
	ASSERT_EQ(2 + 2, 4);
}

TEST_F(QueueClassTest, Enqueue_Test)
{
	MyQueue<int> *myqueue = new MyQueue<int>();
	EXPECT_EQ(myqueue->Count(), 0);

	myqueue->enqueue(2);
	myqueue->enqueue(3);

	EXPECT_EQ(myqueue->Count(), 2);	

	delete myqueue;
}

TEST_F(QueueClassTest, Dequeue_Test)
{
	MyQueue<int> *myqueue = new MyQueue<int>();
	EXPECT_EQ(myqueue->Count(), 0);

	myqueue->enqueue(2);
	myqueue->enqueue(3);

	EXPECT_EQ(myqueue->Count(), 2);

	EXPECT_EQ(myqueue->deQueue(), 2);
	EXPECT_EQ(myqueue->deQueue(), 3);

	EXPECT_EQ(myqueue->Count(), 0);	

	delete myqueue;
}
