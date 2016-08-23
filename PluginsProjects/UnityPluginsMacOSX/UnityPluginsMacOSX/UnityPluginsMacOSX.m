//
//  UnityPluginsMacOSX.m
//  UnityPluginsMacOSX
//
//  Created by wangshibo on 16/8/22.
//  Copyright © 2016年 wangshibo. All rights reserved.
//

#import "UnityPluginsMacOSX.h"

@implementation UnityPluginsMacOSX

@end

#if defined (__cplusplus)
extern "C"
{
#endif
    
    void UnityCallMacOSXPrint()
    {
        NSLog(@"UnityCallMacOSXPrint Succ!");
    }
    
#if defined (__cplusplus)
}
#endif