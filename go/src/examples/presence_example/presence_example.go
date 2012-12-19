package main

import (
	"log"
	"pubnub"
)

func main() {
	pub := pubnub.PubnubInit("demo", "demo", "", "", false)
	channel := make(chan []byte)

	//start new goroutine  
	go pub.Presence("hello_world", channel)

	//receive from channel
	for {
		value, ok := <-channel
		if !ok {
			break
		}
		log.Printf("%s", value)
	}
}
