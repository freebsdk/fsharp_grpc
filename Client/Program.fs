// Learn more about F# at http://fsharp.org

open System
open HelloWorld
open Grpc.Core


type HelloServiceImpl(client : HelloService.HelloServiceClient) =
        member this.SayHello(greeting : string) =
            let request = new HelloReq(Greeting = greeting)
            client.SayHello(request)


[<EntryPoint>]
let main argv =
    let channel = new Channel("127.0.0.1:5000", ChannelCredentials.Insecure)
    let client = new HelloServiceImpl(new HelloService.HelloServiceClient(channel))
    
    let result = client.SayHello("Hi~")
    Console.WriteLine(result.Reply)
    0 // return an integer exit code
