namespace api
{
    public class ConnectionString
    {
        private string host {get; set;}
        private string database {get; set;}
        private string username {get; set;}
        private string port {get; set;}
        private string password {get; set;}

        public string cs {get; set;}

        public ConnectionString(){
        host = "bv2rebwf6zzsv341.cbetxkdyhwsb.us-east-1.rds.amazonaws.com";
        database = "nfjtgg9tq9bwmdn7";
        username = "ce81ciwiwrydyw5e";
        port = "3306";
        password = "si8az8vw89t6df8q";

        cs = $"server={host};user={username};database={database};port={port};password={password}";
        
        }
    }


    
}