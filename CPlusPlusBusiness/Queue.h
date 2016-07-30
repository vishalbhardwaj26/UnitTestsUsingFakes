#pragma once

#include<iostream>
#include<vector>


template<class T>
class MyQueue
{
public:
	MyQueue();
	~MyQueue();
	
	void enqueue(T inputArg);
	T deQueue();
	int Count();

private:
	std::vector<T> elements;
	bool empty ();

};

//#include "Queue.cpp"
//It require .cpp to be removd from projects

template<class T>
MyQueue<T>::MyQueue()
{
	//elements<T>(10);
}

template<class T>
MyQueue<T>::~MyQueue()
{
}


template<class T>
void MyQueue<T>::enqueue(T inputArg)
{
	elements.push_back(inputArg);
}

template<class T>
int MyQueue<T>::Count()
{
	return elements.size();
}

template<class T>
bool MyQueue<T>::empty()
{
	if (Count() == 0)
		return true;

	return false;
}

template<class T>
T MyQueue<T>::deQueue()
{
	if (empty()) {
		throw std::out_of_range("underflow");
	}
	T in = elements.at(0);
	elements.erase(elements.begin());
	return in;
}


