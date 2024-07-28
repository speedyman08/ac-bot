# ac-bot
This is a bot that allows me to control my own air conditioning unit. The way it works is that it sends HTTP requests to a daughter-board 
that has IR leds attached to it. The daughter board has flask running and can make IR messages on the fly because it is programmed to create bytecode that the AC understands
