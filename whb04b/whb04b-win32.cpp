/***************************************************
 * file: whb04b/whb04b-win32.cpp
 *
 * @file    whb04b-win32.cpp
 * @author  Eric L. Hernes
 * @version V1.0
 * @born_on   Saturday, June 3, 2023
 * @copyright (C) Copyright Eric L. Hernes 2023
 * @copyright (C) Copyright Q, Inc. 2023
 *
 * @brief   An Eric L. Hernes Signature Series C++ module
 *
 * https://stackoverflow.com/questions/6507705/callbacks-from-c-back-to-c-sharp
 *
 */

//#define STUB_ONLY

#include <windows.h>
#include "../whb04b.h"
#include "../whb04b-win32.h"

#ifndef STUB_ONLY
#include <future>
static std::future<void> s_runFuture;
#endif

BOOL APIENTRY DllMain(HMODULE hModule, DWORD  ul_reason_for_call, LPVOID lpReserved) {
  return true;
}

class WinDelegate : public WHB04B::Delegate {
public:
  WinDelegate(PendantEventCallback cb) : _cb(cb) {};
  virtual void on_pendant_event(uint8_t b1, uint8_t b2, uint8_t axis, uint8_t stepcon, int count) {
    _cb(b1, b2, axis, stepcon, count);
  }
  PendantEventCallback _cb;
};

static WinDelegate *s_wdp = nullptr;

void*
whb04b_create(PendantEventCallback cb) {
  s_wdp = new WinDelegate(cb);
#ifndef STUB_ONLY
  WHB04B *whb = new WHB04B(s_wdp);
  s_runFuture = std::async(std::launch::async, &WHB04B::run, whb);
  return (void*) whb;
#else
  return 0;
#endif
}

void
whb04b_destroy(void *vp) {
  WHB04B *whb = (WHB04B*)vp;
#ifndef STUB_ONLY
  whb->stop();
  s_runFuture.wait();
  delete whb;
  delete s_wdp;
#endif
}

bool
whb04b_display(void *vp, bool wm_coord, double axis1, double axis2, double axis3, double feed, double spindle) {
#ifndef STUB_ONLY
  ((WHB04B*)vp)->display(wm_coord, axis1, axis2, axis3, feed, spindle);
#endif
  return TRUE;
}

/* end of whb04b/whb04b-win32.cpp */
