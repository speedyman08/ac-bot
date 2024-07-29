# ac-bot
This is a discord bot that allows me to control my own air conditioning unit. The way it works is that it sends HTTP requests to a microcontroller
that has IR leds attached to it. The microcontroller has flask running and can make IR messages on the fly because it is programmed to create bytecode that the AC understands
