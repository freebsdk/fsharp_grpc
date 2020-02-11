open System
open System.Threading.Tasks
open Grpc.Core
open HelloWorld


type HelloServiceImpl() =
    inherit HelloService.HelloServiceBase()
    
    override this.SayHello(request : HelloReq, context :ServerCallContext) : Task<HelloRes> =
        HelloRes(Reply = request.Greeting+" 안녕?") |> Task.FromResult



[<EntryPoint>]
let main argv =
    let port = 5000
    let server = Server()
    server.Services.Add(HelloService.BindService(new HelloServiceImpl()))
    server.Ports.Add(ServerPort("127.0.0.1", port, ServerCredentials.Insecure)) |> ignore
    server.Start()
    
    Console.WriteLine("Press any key to stop the server ...")
    Console.ReadKey() |> ignore
    
    server.ShutdownAsync().Wait()
    0 // return an integer exit code
