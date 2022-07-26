namespace Business.Constans
{
    public static class Messages
    {
        public static string DataListed = "Veri listelendi ";

        public static string AuthorizationDenied = "Yetkiniz yok!";
        public static string UserNotFound = "Kullanıcı bulunamadı" ;
        public static string PasswordError = "Parola hatası";
        public static string UserAlreadyExist = "kullanıcı zaten mevcut";
        public static string EmailNullError = "Email tanımsız olamaz";
        public static string LoggedIn ="Giriş yapıldı";

        public static string UserRegistered { get; internal set; }
        public static string SuccessfulLogin { get; internal set; }
        public static string AccessTokenCreated { get; internal set; }
        public static string UserAlreadyExists { get; internal set; }
    }
}
