// UnityPluginsCLR.h

#pragma once

using namespace System;

namespace UnityPluginsCLR {

	public ref class CLR4orUnity
	{
		// TODO:  在此处添加此类的方法。
	public:
		int UnityCallCppCLRAdd(int a, int b)
		{
			return (a + b);
		}
	};
}
