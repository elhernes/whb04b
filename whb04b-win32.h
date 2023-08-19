/***************************************************
 * file: whb04b/whb04b-win32.h
 *
 * @file    whb04b-win32.h
 * @author  Eric L. Hernes
 * @born_on   Saturday, June 3, 2023
 * @copyright (C) Copyright Eric L. Hernes 2023
 * @copyright (C) Copyright Q, Inc. 2023
 *
 * @brief   An Eric L. Hernes Signature Series C/C++ header
 *
 * $Id$
 */

#pragma once

#include "whb04b.h"

typedef void (__stdcall *PendantEventCallback)(uint8_t b1, uint8_t b2, uint8_t axis, uint8_t stepcon, int count);

extern "C" __declspec(dllexport) void* whb04b_create(PendantEventCallback cb);
extern "C" __declspec(dllexport) void whb04b_destroy(void *vp);
extern "C" __declspec(dllexport) bool whb04b_display(void *vp, bool wm_coord, double axis1, double axis2, double axis3, double feed, double spindle);

//  void pendant_event(uint8_t b1, uint8_t b2, uint8_t axis, uint8_t stepcon, int count);


/* end of whb04b/whb04b-win32.h */
