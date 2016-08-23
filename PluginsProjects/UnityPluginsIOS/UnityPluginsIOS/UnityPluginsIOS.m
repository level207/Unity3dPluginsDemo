//
//  UnityPluginsIOS.m
//  UnityPluginsIOS
//
//  Created by wangshibo on 16/8/22.
//  Copyright © 2016年 wangshibo. All rights reserved.
//

#import "UnityPluginsIOS.h"

@implementation UnityPluginsIOS

@end


#if defined (__cplusplus)
extern "C"
{
#endif
    
    void UnityCallIOSPrint()
    {
        NSLog(@"UnityCallIOSPrint Succ!");
    }
    
#if defined (__cplusplus)
}
#endif
