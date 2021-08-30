/////////////////////////////////////////////////
//// Developer              : Cihan COPUR    ////
//// Creation Date          : 28.08.2021     ////
//// Last Update Date       : 28.08.2021     ////
//// All Rights Reserved ©                   ////
/////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;

namespace ccoftOBJ
{
    public static class SYSTEM_MESSAGE
    {
        public static List<List<string>> MESSAGE_LIST = new List<List<string>>();
        public static void f_gFillMessageList()
        {
            #region 0-NoRecordsFound
            List<string> l_sList = new List<string>();
            l_sList.Add("Kayıt bulunamadı!");
            l_sList.Add("No Records Found!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 1-SystemException
            l_sList = new List<string>();
            l_sList.Add("İşlem esnasında beklenmedik bir hatayla karşılaşıldı! Lütfen tekrar deneyiniz ya da destek için; info@cihancopur.com");
            l_sList.Add("An unexpected error was encountered during the process! Please try again or for support; info@cihancopur.com");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 2-RequiredFields
            l_sList = new List<string>();
            l_sList.Add("Lütfen gerekli alanların doğru bir şekilde doldurunuz!");
            l_sList.Add("Please fill in the required fields correctly!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 3-RegisteredUser
            l_sList = new List<string>();
            l_sList.Add("E-posta adınıza ait mevcut bir üyelik bulunmaktadır!");
            l_sList.Add("There is an existing membership for your email name!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 4-ActivateAccount
            l_sList = new List<string>();
            l_sList.Add("Ailemize hoşgeldiniz :) Lütfen e-posta hesabınıza gelen aktivasyon linkini kullanarak hesabınızı aktifleştiriniz! Spam klasörünü kontrol etmeyi de unutmayınız :)");
            l_sList.Add("Welcome to family :) Please activate your account using the activation link from your e-mail account! Don't forget to check the spam folder :)");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 5-LoginSuccessful
            l_sList = new List<string>();
            l_sList.Add("Oturum açma işlemi başarılı!");
            l_sList.Add("Sign in process successful!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 6-LoginFailed
            l_sList = new List<string>();
            l_sList.Add("Oturum açma işlemi başarısız.Lütfen kullanıcı adı ve şifrenizi tekrar deneyiniz!");
            l_sList.Add("Login failed. Please try your username and password again!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 7-EmailError
            l_sList = new List<string>();
            l_sList.Add("E-posta gönderirken beklenmedik bir hatayla karşılaşıldı!");
            l_sList.Add("An unexpected error was encountered while sending an email!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 8-NotApprovedAccount
            l_sList = new List<string>();
            l_sList.Add("Hesabınız aktif değildir! Lütfen e-posta hesabınıza gelen aktivasyon linkini kullanarak hesabınızı aktifleştiriniz! Spam klasörünü kontrol etmeyi de unutmayınız :)");
            l_sList.Add("Your account is not active! Please activate your account using the activation link from your e-mail account! Don't forget to check the spam folder :)");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 9-PasswordSent
            l_sList = new List<string>();
            l_sList.Add("Şifreniz hesabınıza gönderildi. Spam klasörünü kontrol etmeyi de unutmayınız :)");
            l_sList.Add("Your password send account. Don't forget to check the spam folder :)");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 10-NotAuthorized
            l_sList = new List<string>();
            l_sList.Add("Yetkiniz bulunmamaktadır!");
            l_sList.Add("You are not authorized!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 11-CheckPassword
            l_sList = new List<string>();
            l_sList.Add("Şifreleriniz aynı olmalıdır!");
            l_sList.Add("Passwords should be same!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 12-NoUserFound
            l_sList = new List<string>();
            l_sList.Add("Kullanıcı bulunamadı!");
            l_sList.Add("No Users Found!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 13-ApprovedAccount
            l_sList = new List<string>();
            l_sList.Add("Hesabınız onaylanmıştır :) Oturum açabilirsiniz!");
            l_sList.Add("Your account has been approved :) You can login!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 14-CountCheck
            l_sList = new List<string>();
            l_sList.Add("Aldığı dereceler toplam yarış sayısından fazla olamaz!");
            l_sList.Add("The degrees it received cannot exceed the total number of races!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 15-IsAvailableHorse
            l_sList = new List<string>();
            l_sList.Add("Aşağıda listelenenler arasında görüyorsanız lütfen bu atı eklemeyin!");
            l_sList.Add("If you see it listed below, please do not add this horse!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 16-AvailableRecord
            l_sList = new List<string>();
            l_sList.Add("İlgili kayıt sistemde bulunmaktadır!");
            l_sList.Add("The relevant registration is in the system!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 17-ShouldChangeRequestStatus
            l_sList = new List<string>();
            l_sList.Add("Mevcut talep durumunu değiştirmeden güncelleyemezsiniz!");
            l_sList.Add("You cannot update the current request status without changing it!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 18-Approved
            l_sList = new List<string>();
            l_sList.Add("Kullanıcı hesabı onaylandı!");
            l_sList.Add("User account approved succesfully!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 19-ApprovalRemoved
            l_sList = new List<string>();
            l_sList.Add("Kullanıcı hesabı onayı kaldırıldı!");
            l_sList.Add("User account confirmation removed!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 20-IneligibleRequestStatus
            l_sList = new List<string>();
            l_sList.Add("Talep durumu bu işlem için uygun değildir!");
            l_sList.Add("Request status is not eligible for this process!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 21-AtLeast2Generation
            l_sList = new List<string>();
            l_sList.Add("En az iki nesil sorgulaması yapılmaktadır!");
            l_sList.Add("At least two generations are being BLOG_CATEGORYed!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 22-EmailSent
            l_sList = new List<string>();
            l_sList.Add("Mesajınız başarıyla gönderildi. En kısa sürede tarafınıza dönüş yapılacaktır!");
            l_sList.Add("Your message has been successfully sent. You will be returned as soon as possible!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 23-NotApprovedHorse
            l_sList = new List<string>();
            l_sList.Add("Güncellemek istediğiniz atın mevcut bilgileri henüz onaylanmadığından şuan için güncelleme işlemi yapamazsınız!");
            l_sList.Add("Since the current information of the horse you want to update has not yet been confirmed, you cannot update now!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 24-SuccessfullHorseAddRequest
            l_sList = new List<string>();
            l_sList.Add("At ekleme talebiniz başarıyla tamamlanmıştır!");
            l_sList.Add("Your horse addition request has been successfully completed!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 25-HorseShouldBeYoungerFromParent
            l_sList = new List<string>();
            l_sList.Add("Eklemek istediğiniz atın doğum tarihi anne ve babasından önce olamaz!");
            l_sList.Add("The date of birth of the horse you want to add cannot be before its parents!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 26-SuccessfullHorseUpdateRequest
            l_sList = new List<string>();
            l_sList.Add("At güncelleme talebiniz başarıyla tamamlanmıştır!");
            l_sList.Add("Your horse update request has been successfully completed!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 27-NothingChanged
            l_sList = new List<string>();
            l_sList.Add("Herhangi bir güncelleme yapılmamıştır!");
            l_sList.Add("No updates have been made!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 28-UpdatedProfileSuccessfull
            l_sList = new List<string>();
            l_sList.Add("Profil başarıyla güncellendi!");
            l_sList.Add("Profile has been successfully updated!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 29-CannotCreateReport
            l_sList = new List<string>();
            l_sList.Add("İlgili aygır için bu tipte rapor oluşturamazsınız!");
            l_sList.Add("You cannot create this type of report for the relevant stallion!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 30-ReportCreatedSuccessfully
            l_sList = new List<string>();
            l_sList.Add("Raporunuz başarıyla oluşturuldu!");
            l_sList.Add("Your report has been created successfully!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 31-SuccessfulOrder
            l_sList = new List<string>();
            l_sList.Add("Siparişiniz başarıyla oluşturuldu! Siparişlerim sayfasından takip edebilirsiniz!");
            l_sList.Add("Your order has been successfully created! You can follow it on the My Orders page!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 32-ShouldBeLogin
            l_sList = new List<string>();
            l_sList.Add("İşleminize devam edebilmek için lütfen oturum açınız!");
            l_sList.Add("Please sign in to proceed with your request!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 33-SuccessfullyAddedFavoriteAds
            l_sList = new List<string>();
            l_sList.Add("Favori ilanlarınıza başarıyla eklendi!");
            l_sList.Add("Successfully added to your favorite ads!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 34-SuccessfullyRemovedFavoriteAds
            l_sList = new List<string>();
            l_sList.Add("Favori ilanlarınız arasından başarıyla çıkarıldı!");
            l_sList.Add("Successfully removed from your favorite postings!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 35-AdsStatus1
            l_sList = new List<string>();
            l_sList.Add("Görüntülemek istediğiniz ilan onay bekliyor durumunda olduğu için görüntüleyemezsiniz!");
            l_sList.Add("You cannot view the ad you want to view because it is in a waiting approval status!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 36-AdsStatus3
            l_sList = new List<string>();
            l_sList.Add("Görüntülemek istediğiniz ilanın yayınlanma süresi dolduğu için görüntüleyemezsiniz!");
            l_sList.Add("You cannot view the ad you want to view because the publication period has expired!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 37-AdsStatus4
            l_sList = new List<string>();
            l_sList.Add("Görüntülemek istediğiniz ilan onaylanmadığı için görüntüleyemezsiniz!");
            l_sList.Add("The ad you want to view is not approved, so you cannot view it!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 38-AdsStatus5
            l_sList = new List<string>();
            l_sList.Add("Görüntülemek istediğiniz ilan yayından kaldırıldığı için görüntüleyemezsiniz!");
            l_sList.Add("You cannot view the ad you want to view because it has been removed!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 39-AdsStatus6
            l_sList = new List<string>();
            l_sList.Add("Görüntülemek istediğiniz ilan silindiği için görüntüleyemezsiniz!");
            l_sList.Add("The ad you want to view has been deleted, so you cannot view it!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 40-ClosedBid
            l_sList = new List<string>();
            l_sList.Add("Bu ilan tekliflere kapalıdır!");
            l_sList.Add("This ad is closed to bids!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 41-ShouldBeGreaterMinOffer
            l_sList = new List<string>();
            l_sList.Add("Teklifiniz minimum teklif tutarından büyük olmalıdır!");
            l_sList.Add("Your bid must be greater than the minimum bid amount!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 42-ShouldBeGreaterPreviousOffer
            l_sList = new List<string>();
            l_sList.Add("Teklifiniz mevcut tekliflerden büyük olmalıdır!");
            l_sList.Add("Your bid must be greater than the current offers!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 43-SuccessfullyAddedBid
            l_sList = new List<string>();
            l_sList.Add("Teklifiniz başarıyla iletildi!");
            l_sList.Add("Your bid has been sent successfully!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 44-SuccessfullyRemovedBid
            l_sList = new List<string>();
            l_sList.Add("Teklifiniz başarıyla silindi!");
            l_sList.Add("Your bid has been successfully deleted!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 45-NotAvailableAnyMore
            l_sList = new List<string>();
            l_sList.Add("Erişmek istediğiniz aygır, #EffectiveNick Kayıtlı Aygırları arasında artık yer almamaktadır. Tekrar yer almasını istiyorsanız lütfen aygır sahipleri ile iletişime geçiniz!");
            l_sList.Add("The stallion you want to access is no longer listed among the #EffectiveNick Registered Stallions. Please contact the stallion owners if you want it to take place again!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
            #region 46-SuccessfullyAddedAd
            l_sList = new List<string>();
            l_sList.Add("İlanınız başarıyla eklendi! Onay sürecinden sonra yayına alınacaktır!");
            l_sList.Add("Your ad has been successfully added! It will be published after the approval process!");
            MESSAGE_LIST.Add(l_sList);
            #endregion
        }
    }
}
