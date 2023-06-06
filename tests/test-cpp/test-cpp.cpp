// test-cpp.cpp : Defines the entry point for the console application.
//

#include <thread>
#include <chrono>
using namespace std::chrono_literals;

#include "../../whb04b.h"
#include "../../whb04b-win32.h"

static void __stdcall
callback(uint8_t b1, uint8_t b2, uint8_t axis, uint8_t stepcon, int count) {
  printf("%s: [b1 0x%02x] [b2 0x%02x] [stepcon %02x] [axis %02x] [count %d]\n", __func__,
	 b1, b2, stepcon, axis, count);
}

int main()
{
  void *instance = whb04b_create(callback);
  whb04b_display(instance, true, 12.34, 23.45, 34.56, 3785, 19783);

  for(int i=0; i<10; i++) {
    std::this_thread::sleep_for(1000ms);
  }

  whb04b_destroy(instance);

  return 0;
}

