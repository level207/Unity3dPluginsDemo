// UnityPluginsNative.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "stdlib.h"
#include "malloc.h"

extern "C" __declspec(dllexport) char *UnityCallCppNativeAdd(int a, int b)
{
	/* native为堆中分配的可以 */
	/*char *c = (char *)malloc(sizeof(char) * 20);

	memcpy(c, "helloworld", sizeof("helloworld"));*/

	/* native为局部变量不可以 */
	//char c[20] = "helloworld";

	return "helloworld" ;
}

