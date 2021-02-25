﻿using ComputerUtils.RegxTemplates;
using Microsoft.WindowsAPICodePack.Dialogs;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BMBF_Manager
{
    /// <summary>
    /// Interaktionslogik für BBBU.xaml
    /// </summary>
    public partial class BBBU : Window
    {
        Boolean draggable = true;
        Boolean running = false;
        String exe = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.Length - 1);
        String Songs = "";
        String Playlists = "";
        String Mods = "";
        String Scores = "";
        String APKs = "";
        String BackupF = "";
        JSONNode BackupConfig = JSON.Parse("{}");

        public BBBU()
        {
            InitializeComponent();

            BackupF = exe + "\\BBBUBackups";
            ApplyLanguage();
            Quest.Text = MainWindow.config.IP;
            txtbox.Text = MainWindow.globalLanguage.global.defaultOutputBoxText;
            BName.Text = MainWindow.globalLanguage.bBBU.code.backupNameName;

            if (!Directory.Exists(exe + "\\BBBUBackups"))
            {
                Directory.CreateDirectory(exe + "\\BBBUBackups");
            }
            if (!Directory.Exists(exe + "\\tmp"))
            {
                Directory.CreateDirectory(exe + "\\tmp");
            }
            
            QuestIP();
            TransferFromBBBU();
            Convert();
            getBackups();

            RSongs.IsChecked = true;
            RPlaylists.IsChecked = true;
            RScores.IsChecked = true;
            RMods.IsChecked = true;
            RReplays.IsChecked = true;
            RAPK.IsChecked = false;
            RAPK.Visibility = Visibility.Hidden;

            ChangeImage("BBBU4.png");
        }

        public void ApplyLanguage()
        {
            RestoreB.Content = MainWindow.globalLanguage.bBBU.UI.restoreButton;
            BackupB.Content = MainWindow.globalLanguage.bBBU.UI.backupButton;
            ABackupB.Content = MainWindow.globalLanguage.bBBU.UI.AdvancedBackupButton;
            RSongs.Content = MainWindow.globalLanguage.bBBU.UI.restoreSongsBox;
            RPlaylists.Content = MainWindow.globalLanguage.bBBU.UI.restorePlaylistsBox;
            RScores.Content = MainWindow.globalLanguage.bBBU.UI.restoreScoresBox;
            RMods.Content = MainWindow.globalLanguage.bBBU.UI.restoreModsBox;
            RReplays.Content = MainWindow.globalLanguage.bBBU.UI.restoreModDataBox;
            RAPK.Content = MainWindow.globalLanguage.bBBU.UI.restoreVersionBox;
        }

        public void TransferFromBBBU()
        {
            if (MainWindow.config.BBBUTransfered) return;
            MainWindow.config.BBBUTransfered = true;
            MessageBoxResult r = MessageBox.Show(MainWindow.globalLanguage.bBBU.code.importBBBUQuestion, "BMBF Manager - BMBF Beat Saber Backup Utility", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (r)
            {
                case MessageBoxResult.No:
                    txtbox.AppendText("\n\n" + MainWindow.globalLanguage.bBBU.code.nothingImported);
                    return;
            }
            MessageBox.Show(MainWindow.globalLanguage.bBBU.code.selectBBBUFolderInfo, "BMBF Manager - BMBF Beat Saber Backup Utility", MessageBoxButton.OK, MessageBoxImage.Information);

            CommonOpenFileDialog ofd = new CommonOpenFileDialog();
            ofd.IsFolderPicker = true;
            if (ofd.ShowDialog() == CommonFileDialogResult.Ok)
            {
                //Get the path of specified file
                if (Directory.Exists(ofd.FileName))
                {
                    foreach(String folder in Directory.GetDirectories(ofd.FileName))
                    {
                        Console.WriteLine(folder);
                        String backupName = new DirectoryInfo(folder).Name;
                        Directory.Move(folder, exe + "\\BBBUBackups\\" + backupName);
                        txtbox.AppendText("\n\n" + MainWindow.globalLanguage.processer.ReturnProcessed(MainWindow.globalLanguage.bBBU.code.movedBackup, backupName));
                    }
                }
                else
                {
                    MessageBox.Show(MainWindow.globalLanguage.bBBU.code.selectValidDir, "BMBF Manager - BMBF Beat Saber backup Utility", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }

            txtbox.AppendText("\n\n" + MainWindow.globalLanguage.bBBU.code.backupsMoved);
        }

        public void Convert()
        {
            foreach(String file in Directory.GetDirectories(exe + "\\BBBUBackups"))
            {
                if (file.EndsWith(".json"))
                {
                    String contents = File.ReadAllText(file);
                    if (contents.EndsWith(","))
                    {
                        contents = contents.Substring(0, contents.Length - 1) + "}}";
                        JSONNode c = JSON.Parse(contents);
                        File.Delete(file);
                        File.WriteAllText(file, c["Config"].ToString());
                    }
                }
            }
        }

        public void BackupLink(String Name)
        {
            StartBackup(Name, false);
        }

        public void ABackupLink(String Name)
        {
            StartBackup(Name, true);
        }

        public void Backup(object sender, RoutedEventArgs e)
        {
            StartBackup("", false);
        }

        public void ABackup(object sender, RoutedEventArgs e)
        {
            StartBackup("", true);
        }

        public void StartBackup(String BackupName, Boolean Advanced)
        {
            StartBMBF();
            if (running)
            {
                running = false;
                return;
            }
            running = true;

            if(BackupName != "")
            {
                BName.Text = Name;
            }

            //Check Quest IP
            if (!MainWindow.iPUtils.CheckIP(Quest))
            {
                txtbox.AppendText("\n\n" + MainWindow.globalLanguage.global.ipInvalid);
                running = false;
                return;
            }

            if(Advanced)
            {
                MessageBoxResult r = MessageBox.Show(MainWindow.globalLanguage.bBBU.code.advancedBackupWarning, "BMBF Manager - BMBF Beat Saber Backup Utility", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                switch (r)
                {
                    case MessageBoxResult.No:
                        txtbox.AppendText("\n\n" + MainWindow.globalLanguage.bBBU.code.backupAborted);
                        running = false;
                        return;
                }
            }

            MainWindow.DCRPM.SetActivity(MainWindow.globalLanguage.dCRP.creatingBBBUBackup);

            //Create all Backup Folders
            if (!BackupFSet())
            {
                txtbox.AppendText("\n\n" + MainWindow.globalLanguage.bBBU.code.backupExists);
                running = false;
                return;
            }

            BackupConfig = JSON.Parse("{}");

            //Scores
            txtbox.AppendText("\n\n" + MainWindow.globalLanguage.bBBU.code.backingUpScores);
            MainWindow.aDBI.adb("pull /sdcard/Android/data/com.beatgames.beatsaber/files/LocalDailyLeaderboards.dat \"" + Scores + "\"", txtbox);
            MainWindow.aDBI.adb("pull /sdcard/Android/data/com.beatgames.beatsaber/files/LocalLeaderboards.dat \"" + Scores + "\"", txtbox);
            MainWindow.aDBI.adb("pull /sdcard/Android/data/com.beatgames.beatsaber/files/PlayerData.dat \"" + Scores + "\"", txtbox);
            MainWindow.aDBI.adb("pull /sdcard/Android/data/com.beatgames.beatsaber/files/AvatarData.dat \"" + Scores + "\"", txtbox);
            MainWindow.aDBI.adb("pull /sdcard/Android/data/com.beatgames.beatsaber/files/settings.cfg \"" + Scores + "\"", txtbox);
            txtbox.AppendText("\n" + MainWindow.globalLanguage.bBBU.code.backedUpScores + "\n");
            txtbox.ScrollToEnd();

            //Songs

            QSE();
            //MainWindow.aDBI.adb("pull /sdcard/BMBFData/CustomSongs \"" + BackupF + "\"");

            //Playlists

            PlaylistB();
            MainWindow.aDBI.adb("pull /sdcard/BMBFData/Playlists/ \"" + Playlists + "\"", txtbox);
            txtbox.ScrollToEnd();

            //ModData

            txtbox.AppendText("\n\n" + MainWindow.globalLanguage.bBBU.code.backingUpModData);
            MainWindow.aDBI.adb("pull /sdcard/ModData/com.beatgames.beatsaber \"" + BackupF + "\"", txtbox);
            txtbox.AppendText("\n" + MainWindow.globalLanguage.bBBU.code.backedUpModData + "\n");
            txtbox.ScrollToEnd();

            //Mods

            ModsB();

            if (Advanced)
            {
                BackupAPK();
                if(!BackupConfig["BMBFBackup"])
                {
                    MessageBox.Show(MainWindow.globalLanguage.bBBU.code.bMBFAPKBackupFailed, "BMBF Manager - BMBF Beat Saber Backup Utility", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (!BackupConfig["BSBackup"])
                {
                    MessageBox.Show(MainWindow.globalLanguage.bBBU.code.bSAPKBackupFailed, "BMBF Manager - BMBF Beat Saber Backup Utility", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            if(Advanced)
            {
                BackupConfig["BackupType"] = 1;
            } else
            {
                BackupConfig["BackupType"] = 0;
            }
            BackupConfig["Qosmetics"] = true;
            File.WriteAllText(BackupF + "\\Backup.json", BackupConfig.ToString());

            txtbox.AppendText("\n\n\n" + MainWindow.globalLanguage.bBBU.code.backupMade);
            txtbox.ScrollToEnd();
            running = false;
        }

        public void selectBackup(String Name)
        {
            for(int i = 0; Backups.Items.Count > i; i++)
            {
                Backups.SelectedIndex = i + 1;
                if(Backups.SelectedValue.ToString() == Name)
                {
                    Backups.SelectedIndex = i + 1;
                    break;
                }
            }
        }

        public void Restore(object sender, RoutedEventArgs e)
        {
            StartBMBF();
            if (running)
            {
                return;
            }
            running = true;

            if (Backups.SelectedIndex == 0)
            {
                txtbox.AppendText("\n\n" + MainWindow.globalLanguage.bBBU.code.selectValidBackup);
                running = false;
                return;
            }

            //Get Backup Folders
            BackupFGet();

            //Check Quest IP
            if (!MainWindow.iPUtils.CheckIP(Quest))
            {
                txtbox.AppendText("\n\n" + MainWindow.globalLanguage.global.ipInvalid);
                running = false;
                return;
            }

            if ((bool)RAPK.IsChecked)
            {
                MessageBoxResult r = MessageBox.Show(MainWindow.globalLanguage.bBBU.code.rAPKWarning, "BMBF Manager - BMBF Beat Saber Backup Utility", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                switch (r)
                {
                    case MessageBoxResult.Yes:
                        txtbox.AppendText("\n\n" + MainWindow.globalLanguage.bBBU.code.restoringAborted);
                        return;
                }
            }

            MainWindow.DCRPM.SetActivity(MainWindow.globalLanguage.dCRP.restoringBBBUBackup);

            //APKs
            if ((bool)RAPK.IsChecked)
            {
                RestoreAPK();
            }

            //Scores
            if ((bool)RScores.IsChecked == true && (bool)RAPK.IsChecked == false)
            {
                txtbox.AppendText("\n\n" + MainWindow.globalLanguage.bBBU.code.pushingScores);
                MainWindow.aDBI.adb("push \"" + Scores + "\\LocalDailyLeaderboards.dat\" /sdcard/Android/data/com.beatgames.beatsaber/files/LocalDailyLeaderboards.dat", txtbox);
                MainWindow.aDBI.adb("push \"" + Scores + "\\LocalLeaderboards.dat\" /sdcard/Android/data/com.beatgames.beatsaber/files/LocalLeaderboards.dat", txtbox);
                MainWindow.aDBI.adb("push \"" + Scores + "\\PlayerData.dat\" /sdcard/Android/data/com.beatgames.beatsaber/files/PlayerData.dat", txtbox);
                MainWindow.aDBI.adb("push \"" + Scores + "\\AvatarData.dat\" /sdcard/Android/data/com.beatgames.beatsaber/files/AvatarData.dat", txtbox);
                MainWindow.aDBI.adb("push \"" + Scores + "\\settings.cfg\" /sdcard/Android/data/com.beatgames.beatsaber/files/setting.cfg", txtbox);
                txtbox.AppendText("\n" + MainWindow.globalLanguage.bBBU.code.pushedScores);
                txtbox.ScrollToEnd();
            }

            //ModData
            if ((bool)RReplays.IsChecked)
            {
                txtbox.AppendText("\n\n" + MainWindow.globalLanguage.bBBU.code.pushingModData);
                if (Directory.Exists(BackupF + "\\replays")) MainWindow.aDBI.adb("push \"" + BackupF + "\\replays\" /sdcard/ModData/com.beatgames.beatsaber/Mods/Replay/", txtbox);
                if (Directory.Exists(BackupF + "\\Configs")) MainWindow.aDBI.adb("push \"" + BackupF + "\\Configs\" /sdcard/ModData/com.beatgames.beatsaber/", txtbox);
                if (Directory.Exists(BackupF + "\\Qosmetics")) MainWindow.aDBI.adb("push \"" + BackupF + "\\Qosmetics\" /sdcard/ModData/com.beatgames.beatsaber/Mods/", txtbox);
                if (Directory.Exists(BackupF + "\\com.beatgames.beatsaber")) MainWindow.aDBI.adb("push \"" + BackupF + "\\com.beatgames.beatsaber\" /sdcard/ModData/", txtbox);
                txtbox.AppendText("\n" + MainWindow.globalLanguage.bBBU.code.pushedModData);
                txtbox.ScrollToEnd();
            }

            //Songs
            if ((bool)RSongs.IsChecked)
            {
                if (CheckVer() == 0)
                {
                    txtbox.AppendText("\n\n" + MainWindow.globalLanguage.bBBU.code.uploadingSongs);
                    Upload(Songs);
                    txtbox.AppendText("\n" + MainWindow.globalLanguage.bBBU.code.uploadedSongs);
                }
                else if (CheckVer() == 1)
                {
                    txtbox.AppendText("\n" + MainWindow.globalLanguage.bBBU.code.pushingSongs);
                    MainWindow.aDBI.adb("push \"" + Songs + "\" /sdcard/BMBFData", txtbox);
                    txtbox.AppendText("\n" + MainWindow.globalLanguage.bBBU.code.pushedSongs);
                }
                txtbox.ScrollToEnd();
            }

            //Playlists
            if ((bool)RPlaylists.IsChecked)
            {
                PlaylistsR();
                PushPNG(Playlists + "\\Playlists");
                txtbox.ScrollToEnd();
            }

            //Mods
            if ((bool)RMods.IsChecked)
            {
                txtbox.AppendText("\n\n" + MainWindow.globalLanguage.bBBU.code.uploadingMods);
                Upload(Mods);
                txtbox.AppendText("\n" + MainWindow.globalLanguage.bBBU.code.uploadedMods);
                txtbox.ScrollToEnd();
            }

            txtbox.AppendText("\n\n\n" + MainWindow.globalLanguage.bBBU.code.restored);
            txtbox.ScrollToEnd();
            running = false;
        }

        public void BackupAPK()
        {
            BackupConfig["BSBackup"] = false;
            BackupConfig["BMBFBackup"] = false;
            txtbox.AppendText("\n\n" + MainWindow.globalLanguage.bBBU.code.backingUpBSAPK);
            String moddedBS = MainWindow.aDBI.adbS("shell pm path com.beatgames.beatsaber", txtbox).Replace("package:", "").Replace(System.Environment.NewLine, "");
            if (MainWindow.aDBI.adb("pull " + moddedBS + " \"" + APKs + "\\BeatSaber.apk\"", txtbox))
            {
                BackupConfig["BSBackup"] = true;
                if(!File.Exists(APKs + "\\BeatSaber.apk")) BackupConfig["BSBackup"] = false;
                txtbox.AppendText("\n" + MainWindow.globalLanguage.bBBU.code.backedUpBSAPK);
            }

            txtbox.AppendText("\n\n" + MainWindow.globalLanguage.bBBU.code.backingUpBMBFAPK);
            String BMBF = MainWindow.aDBI.adbS("shell pm path com.weloveoculus.BMBF", txtbox).Replace("package:", "").Replace(System.Environment.NewLine, "");
            if (MainWindow.aDBI.adb("pull " + BMBF + " \"" + APKs + "\\BMBF.apk\"", txtbox))
            {
                BackupConfig["BMBFBackup"] = true;
                if (!File.Exists(APKs + "\\BMBF.apk")) BackupConfig["BMBFBackup"] = false;
                txtbox.AppendText("\n" + MainWindow.globalLanguage.bBBU.code.backedUpBMBFAPK);
            }

            txtbox.AppendText("\n\n" + MainWindow.globalLanguage.bBBU.code.backingUpGameData);
            if (MainWindow.aDBI.adb("pull /sdcard/Android/data/com.beatgames.beatsaber/files \"" + BackupF + "\"", txtbox))
            {
                txtbox.AppendText("\n" + MainWindow.globalLanguage.bBBU.code.backedUpGameData);
            }
        }

        public void RestoreAPK()
        {
            if (!BackupConfig["BSBackup"].AsBool)
            {
                MessageBox.Show(MainWindow.globalLanguage.bBBU.code.bSAPKNotFound, "BMBF Manager - BMBF Beat Saber Backup Utility", MessageBoxButton.OK, MessageBoxImage.Error);
                txtbox.AppendText("\n\n" + MainWindow.globalLanguage.bBBU.code.aPKRestoringAborted);
                return;
            }
            txtbox.AppendText("\n\n" + MainWindow.globalLanguage.bBBU.code.backingUpScores);
            MainWindow.aDBI.adb("pull /sdcard/Android/data/com.beatgames.beatsaber/files/LocalDailyLeaderboards.dat \"" + Scores + "\"", txtbox);
            MainWindow.aDBI.adb("pull /sdcard/Android/data/com.beatgames.beatsaber/files/LocalLeaderboards.dat \"" + Scores + "\"", txtbox);
            MainWindow.aDBI.adb("pull /sdcard/Android/data/com.beatgames.beatsaber/files/PlayerData.dat \"" + Scores + "\"", txtbox);
            MainWindow.aDBI.adb("pull /sdcard/Android/data/com.beatgames.beatsaber/files/AvatarData.dat \"" + Scores + "\"", txtbox);
            MainWindow.aDBI.adb("pull /sdcard/Android/data/com.beatgames.beatsaber/files/settings.cfg \"" + Scores + "\"", txtbox);
            txtbox.AppendText("\n" + MainWindow.globalLanguage.bBBU.code.backedUpScores);

            txtbox.AppendText("\n\n" + MainWindow.globalLanguage.bBBU.code.installingBS);
            if (!MainWindow.aDBI.adb("uninstall com.beatgames.beatsaber", txtbox)) return;
            if (!MainWindow.aDBI.adb("install \"" + APKs + "\\BeatSaber.apk\"", txtbox)) return;
            txtbox.AppendText("\n" + MainWindow.globalLanguage.bBBU.code.installedBS);

            txtbox.AppendText("\n\n" + MainWindow.globalLanguage.bBBU.code.installingBMBF);
            if (!MainWindow.aDBI.adb("uninstall com.weloveoculus.BMBF", txtbox)) return;
            if (!MainWindow.aDBI.adb("install \"" + APKs + "\\BMBF.apk\"", txtbox)) return;
            txtbox.AppendText("\n" + MainWindow.globalLanguage.bBBU.code.installedBMBF);

            txtbox.AppendText("\n\n" + MainWindow.globalLanguage.bBBU.code.restoringGameData);
            if (!MainWindow.aDBI.adb("push \"" + BackupF + "\\files\" /sdcard/Android/data/com.beatgames.beatsaber/", txtbox)) return;
            if (!MainWindow.aDBI.adb("push \"" + BackupF + "\\com.beatgames.beatsaber\" /sdcard/ModData/", txtbox)) return;
            txtbox.AppendText("\n" + MainWindow.globalLanguage.bBBU.code.restoredGameData);

            txtbox.AppendText("\n\n" + MainWindow.globalLanguage.bBBU.code.pushingScores);
            MainWindow.aDBI.adb("push \"" + Scores + "\\LocalDailyLeaderboards.dat\" /sdcard/Android/data/com.beatgames.beatsaber/files/LocalDailyLeaderboards.dat", txtbox);
            MainWindow.aDBI.adb("push \"" + Scores + "\\LocalLeaderboards.dat\" /sdcard/Android/data/com.beatgames.beatsaber/files/LocalLeaderboards.dat", txtbox);
            MainWindow.aDBI.adb("push \"" + Scores + "\\PlayerData.dat\" /sdcard/Android/data/com.beatgames.beatsaber/files/PlayerData.dat", txtbox);
            MainWindow.aDBI.adb("push \"" + Scores + "\\AvatarData.dat\" /sdcard/Android/data/com.beatgames.beatsaber/files/AvatarData.dat", txtbox);
            MainWindow.aDBI.adb("push \"" + Scores + "\\AvatarData.dat\" /sdcard/Android/data/com.beatgames.beatsaber/files/setting.cfg", txtbox);
            txtbox.AppendText("\n" + MainWindow.globalLanguage.bBBU.code.pushedScores);
        }

        public void QuestIP()
        {
            String IPS = MainWindow.aDBI.adbS("shell ifconfig wlan0", txtbox);
            int Index = IPS.IndexOf("inet addr:");
            Boolean space = false;
            String FIP = "";
            for (int i = 0; i < IPS.Length; i++)
            {
                if (i > (Index + 9) && i < (Index + 9 + 16))
                {
                    if (IPS.Substring(i, 1) == " ")
                    {
                        space = true;
                    }
                    if (!space)
                    {
                        FIP = FIP + IPS.Substring(i, 1);
                    }
                }
            }

            if (FIP == "")
            {
                return;
            }
            MainWindow.config.IP = FIP;
            Quest.Text = FIP;
            if (MainWindow.config.IP == MainWindow.globalLanguage.global.defaultQuestIPText)
            {
                Quest.Text = MainWindow.globalLanguage.global.defaultQuestIPText;
            }

        }

        public void StartBMBF()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate
            {
                MainWindow.aDBI.adb("shell am start -n com.weloveoculus.BMBF/com.weloveoculus.BMBF.MainActivity", txtbox);
            }));
        }

        public int CheckVer()
        {
            String[] directories = Directory.GetFiles(Songs);



            for (int i = 0; i < directories.Length; i++)
            {
                if (directories[i].EndsWith(".zip"))
                {
                    return 0;
                }
            }
            return 1;
        }

        public void PushPNG(String Path)
        {
            String[] directories = Directory.GetFiles(Path);



            for (int i = 0; i < directories.Length; i++)
            {
                if (directories[i].EndsWith(".png"))
                {
                    txtbox.AppendText("\n\n" + MainWindow.globalLanguage.processer.ReturnProcessed(MainWindow.globalLanguage.mainMenu.code.pushingPng, directories[i]));
                    MainWindow.aDBI.adb("push \"" + directories[i] + "\" /sdcard/BMBFData/Playlists/", txtbox);
                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate { }));
                }
            }
        }

        public void PlaylistsR()
        {
            try
            {
                


                String PlaylistsX;

                txtbox.AppendText("\n\n" + MainWindow.globalLanguage.processer.ReturnProcessed(MainWindow.globalLanguage.bBBU.code.restoringPlaylists, Playlists + "\\Playlists.json"));
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate { }));



                if (!Directory.Exists(exe + "\\tmp"))
                {
                    Directory.CreateDirectory(exe + "\\tmp");
                }
                WebClient client = new WebClient();

                PlaylistsX = Playlists + "\\Playlists.json";

                var j = JSON.Parse(client.DownloadString("http://" + MainWindow.config.IP + ":50000/host/beatsaber/config"));
                var p = JSON.Parse(File.ReadAllText(PlaylistsX));

                j["Config"]["Playlists"] = p["Playlists"];
                File.WriteAllText(exe + "\\tmp\\config.json", j["Config"].ToString());

                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate {
                    postChanges(exe + "\\tmp\\config.json");
                }));
                txtbox.AppendText("\n\n" + MainWindow.globalLanguage.mainMenu.code.playlistsRestored);
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate { }));
            }
            catch
            {
                txtbox.AppendText(MainWindow.globalLanguage.global.BMBF100);
            }
        }

        public void postChanges(String Config)
        {
            using (WebClient client = new WebClient())
            {
                client.QueryString.Add("foo", "foo");
                client.UploadFile("http://" + MainWindow.config.IP + ":50000/host/beatsaber/config", "PUT", Config);
                client.UploadValues("http://" + MainWindow.config.IP + ":50000/host/beatsaber/commitconfig", "POST", client.QueryString);
            }
        }
        public void Sync()
        {
            System.Threading.Thread.Sleep(2000);
            using (WebClient client = new WebClient())
            {
                client.QueryString.Add("foo", "foo");
                client.UploadValues("http://" + MainWindow.config.IP + ":50000/host/beatsaber/commitconfig", "POST", client.QueryString);
            }
        }

        public void Upload(String Path)
        {
            
            String[] directories = Directory.GetFiles(Path);


            for (int i = 0; i < directories.Length; i++)
            {
                WebClient client = new WebClient();

                txtbox.AppendText("\n\n" + MainWindow.globalLanguage.processer.ReturnProcessed(MainWindow.globalLanguage.bBBU.code.uploadingToBMBF, directories[i]));
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate {
                    try
                    {
                        client.UploadFile("http://" + MainWindow.config.IP + ":50000/host/beatsaber/upload?overwrite", directories[i]);
                        return;
                    }
                    catch
                    {
                        txtbox.AppendText(MainWindow.globalLanguage.global.BMBF100);
                    }
                }));

                if (i % 20 == 0 && i != 0)
                {
                    txtbox.AppendText("\n\n" + MainWindow.globalLanguage.global.syncingToBS);
                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate { }));
                    Sync();
                    System.Threading.Thread.Sleep(2000);
                }
            }
            Sync();
        }

        public void BackupFGet()
        {

            BackupF = exe + "\\BBBUBackups\\" + Backups.SelectedValue;
            Songs = BackupF + "\\CustomSongs";
            Mods = BackupF + "\\Mods";
            Scores = BackupF + "\\Scores";
            Playlists = BackupF + "\\Playlists";
            APKs = BackupF + "\\APK";
        }

        public void ModsB()
        {
            ArrayList list = new ArrayList();
            int overwritten = 0;
            int exported = 0;
            String Name = "";
            String Source = "";

            txtbox.AppendText("\n" + MainWindow.globalLanguage.processer.ReturnProcessed(MainWindow.globalLanguage.bBBU.code.copyingModsToTMP, exe + "\\tmp"));
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate { }));
            MainWindow.aDBI.adb("pull /sdcard/BMBFData/Mods/ \"" + exe + "\\tmp\"", txtbox);
            if (Directory.Exists(exe + "\\tmp\\Mods"))
            {
                Source = exe + "\\tmp\\Mods";
            }
            else
            {
                Source = exe + "\\tmp";
            }


            foreach (String cd in Directory.GetDirectories(Source))
            {
                txtbox.AppendText("\n");

                try
                {
                    JSONNode bmbfmod = JSON.Parse(File.ReadAllText(cd + "\\" + "bmbfmod.json"));
                    Name = bmbfmod["name"];

                    Name = Name.Replace("/", "");
                    Name = Name.Replace(":", "");
                    Name = Name.Replace("*", "");
                    Name = Name.Replace("?", "");
                    Name = Name.Replace("\"", "");
                    Name = Name.Replace("<", "");
                    Name = Name.Replace(">", "");
                    Name = Name.Replace("|", "");
                    Name = Name.Replace(@"\", "");
                    Name = Name.Trim();

                    int Time = 0;
                    while (list.Contains(Name.ToLower()))
                    {
                        Time++;
                        if (Time > 1)
                        {
                            Name = Name.Substring(0, Name.Length - 1);
                            Name = Name + Time;
                        }
                        else
                        {
                            Name = Name + " " + Time;
                        }

                    }
                    list.Add(Name.ToLower());
                    txtbox.AppendText("\n" + MainWindow.globalLanguage.processer.ReturnProcessed(MainWindow.globalLanguage.bBBU.code.modName, Name));
                    txtbox.AppendText("\n" + MainWindow.globalLanguage.processer.ReturnProcessed(MainWindow.globalLanguage.bBBU.code.folder, cd));

                    bool v = File.Exists(Mods + "\\" + Name + ".zip");
                    if (v)
                    {
                        File.Delete(Mods + "\\" + Name + ".zip");
                        txtbox.AppendText("\n" + MainWindow.globalLanguage.processer.ReturnProcessed(MainWindow.globalLanguage.bBBU.code.fileOverwritten, Mods + "\\" + Name + ".zip"));

                        overwritten++;
                    }

                    zip(cd, Mods + "\\" + Name + ".zip");
                    exported++;
                    Name = "";
                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate { }));
                    txtbox.ScrollToEnd();
                }
                catch
                {

                }


            }

            txtbox.AppendText("\n\n\n" + MainWindow.globalLanguage.processer.ReturnProcessed(MainWindow.globalLanguage.bBBU.code.finishedModBackup, exported.ToString()));
            txtbox.ScrollToEnd();
        }

        public void PlaylistB()
        {
            try
            {
                

                txtbox.AppendText("\n\n" + MainWindow.globalLanguage.processer.ReturnProcessed(MainWindow.globalLanguage.mainMenu.code.playlistBackup, Playlists + "\\Playlists.json"));
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate { }));



                if (!Directory.Exists(exe + "\\tmp"))
                {
                    Directory.CreateDirectory(exe + "\\tmp");
                }
                WebClient client = new WebClient();

                var j = JSON.Parse(client.DownloadString("http://" + MainWindow.config.IP + ":50000/host/beatsaber/config"));
                File.WriteAllText(Playlists + "\\Playlists.json", j["Config"].ToString()); 
                txtbox.AppendText("\n\n" + MainWindow.globalLanguage.processer.ReturnProcessed(MainWindow.globalLanguage.mainMenu.code.playlistBackupFinished, Playlists + "\\Playlists.json"));
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate { }));
            }
            catch
            {
                txtbox.AppendText(MainWindow.globalLanguage.global.BMBF100);

            }
            getBackups();
        }

        public void QSE()
        {
            ArrayList list = new ArrayList();
            ArrayList content = new ArrayList();
            ArrayList over = new ArrayList();
            int exported = 0;
            String Name = "";
            String Source = "";

            txtbox.AppendText("\n" + MainWindow.globalLanguage.processer.ReturnProcessed(MainWindow.globalLanguage.bBBU.code.copyingSongsToTMP, exe + "\\tmp"));
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate { }));
            MainWindow.aDBI.adb("pull /sdcard/BMBFData/CustomSongs/ \"" + exe + "\\tmp\"", txtbox);
            if (Directory.Exists(exe + "\\tmp\\CustomSongs"))
            {
                Source = exe + "\\tmp\\CustomSongs";
            }
            else
            {
                Source = exe + "\\tmp";
            }

            foreach (String cd in Directory.GetDirectories(Source))
            {
                txtbox.AppendText("\n");


                if (!File.Exists(cd + "\\" + "Info.dat") && !File.Exists(cd + "\\" + "info.dat"))
                {
                    txtbox.AppendText("\n" + MainWindow.globalLanguage.processer.ReturnProcessed(MainWindow.globalLanguage.bBBU.code.isNotSongs, cd));
                    continue;
                }
                String dat = "";
                if (File.Exists(cd + "\\" + "Info.dat"))
                {
                    dat = cd + "\\" + "Info.dat";

                }
                if (File.Exists(cd + "\\" + "info.dat"))
                {
                    dat = cd + "\\" + "info.dat";

                }
                try
                {
                    var json = SimpleJSON.JSON.Parse(File.ReadAllText(dat));
                    Name = json["_songName"].ToString();

                    Name = Name.Replace("/", "");
                    Name = Name.Replace(":", "");
                    Name = Name.Replace("*", "");
                    Name = Name.Replace("?", "");
                    Name = Name.Replace("\"", "");
                    Name = Name.Replace("<", "");
                    Name = Name.Replace(">", "");
                    Name = Name.Replace("|", "");
                    Name = Name.Replace(@"\", "");
                    Name = Name.Trim();
                    int Time = 0;

                    while (list.Contains(Name.ToLower()))
                    {
                        Time++;
                        if (Time > 1)
                        {
                            Name = Name.Substring(0, Name.Length - 1);
                            Name = Name + Time;
                        }
                        else
                        {
                            Name = Name + " " + Time;
                        }

                    }
                    list.Add(Name.ToLower());
                    txtbox.AppendText("\n" + MainWindow.globalLanguage.processer.ReturnProcessed(MainWindow.globalLanguage.bBBU.code.songName, Name));
                    txtbox.AppendText("\n" + MainWindow.globalLanguage.processer.ReturnProcessed(MainWindow.globalLanguage.bBBU.code.folder, cd));



                    zip(cd, Songs + "\\" + Name + ".zip");
                    exported++;
                    Name = "";
                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate { }));
                    txtbox.ScrollToEnd();
                }
                catch
                {

                }
            }
            txtbox.AppendText("\n\n");

            if (exported == 0)
            {
                txtbox.AppendText(MainWindow.globalLanguage.global.QSU100);
            }
            else
            {
                txtbox.AppendText("\n" + MainWindow.globalLanguage.processer.ReturnProcessed(MainWindow.globalLanguage.bBBU.code.finishedSongBackup, exported.ToString()));
            }
            txtbox.ScrollToEnd();
        }

        public static void zip(String src, String Output)
        {
            ZipFile.CreateFromDirectory(src, Output);

        }

        public String Strings(String line, int StartIndex)
        {
            int count = 0;
            String Name = "";
            for (int n = 0; n < line.Length; n++)
            {
                if (count == StartIndex)
                {

                    Name = Name + line.Substring(n, 1);
                }

                if (line.Substring(n, 1).Equals("\""))
                {
                    count++;
                }
            }
            return Name;
        }

        public Boolean BackupFSet()
        {
            BName.Text = BName.Text.Replace("/", "");
            BName.Text = BName.Text.Replace(":", "");
            BName.Text = BName.Text.Replace("*", "");
            BName.Text = BName.Text.Replace("?", "");
            BName.Text = BName.Text.Replace("\"", "");
            BName.Text = BName.Text.Replace("<", "");
            BName.Text = BName.Text.Replace(">", "");
            BName.Text = BName.Text.Replace("|", "");
            BName.Text = BName.Text.Replace(@"\", "");
            BName.Text = BName.Text.Trim();

            BackupF = exe + "\\BBBUBackups\\" + BName.Text;

            if (Directory.Exists(BackupF))
            {
                return false;
            }

            Songs = BackupF + "\\CustomSongs";
            Mods = BackupF + "\\Mods";
            Scores = BackupF + "\\Scores";
            Playlists = BackupF + "\\Playlists";
            APKs = BackupF + "\\APK";

            if (!Directory.Exists(Songs))
            {
                Directory.CreateDirectory(Songs);
            }
            if (!Directory.Exists(Mods))
            {
                Directory.CreateDirectory(Mods);
            }
            if (!Directory.Exists(Scores))
            {
                Directory.CreateDirectory(Scores);
            }
            if (!Directory.Exists(Playlists))
            {
                Directory.CreateDirectory(Playlists);
            }
            if (!Directory.Exists(APKs))
            {
                Directory.CreateDirectory(APKs);
            }
            return true;
        }

        public void getBackups()
        {
            string[] Files = Directory.GetDirectories(exe + "\\BBBUBackups");
            Backups.Items.Clear();
            Backups.Items.Add(MainWindow.globalLanguage.bBBU.code.backupName);

            for (int i = 0; i < Files.Length; i++)
            {
                Backups.Items.Add(Files[i].Substring(Files[i].LastIndexOf("\\") + 1, Files[i].Length - 1 - Files[i].LastIndexOf("\\")));
            }

            Backups.SelectedIndex = 0;
        }

        public void getQuestIP()
        {
            MainWindow.config.IP = Quest.Text;
        }

        
        private void Mini(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void QuestIPCheck(object sender, RoutedEventArgs e)
        {
            if (Quest.Text == "")
            {
                Quest.Text = MainWindow.globalLanguage.global.defaultQuestIPText;
            }
        }

        private void BackupNameCheck(object sender, RoutedEventArgs e)
        {
            if (BName.Text == "")
            {
                BName.Text = MainWindow.globalLanguage.bBBU.code.backupNameName;
            }
        }

        public void noDrag(object sender, MouseEventArgs e)
        {
            draggable = false;
        }

        public void doDrag(object sender, MouseEventArgs e)
        {
            draggable = true;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            MainWindow.iPUtils.CheckIP(Quest);
            this.Close();
        }

        private void Drag(object sender, RoutedEventArgs e)
        {
            bool mouseIsDown = System.Windows.Input.Mouse.LeftButton == MouseButtonState.Pressed;


            if (mouseIsDown)
            {
                if (draggable)
                {
                    this.DragMove();
                }

            }

        }

        private void ClearText(object sender, RoutedEventArgs e)
        {
            if (Quest.Text == MainWindow.globalLanguage.global.defaultQuestIPText)
            {
                Quest.Text = "";
            }

        }

        private void ClearTextN(object sender, RoutedEventArgs e)
        {
            if (BName.Text == MainWindow.globalLanguage.bBBU.code.backupNameName)
            {
                BName.Text = "";
            }
        }

        private void GetBackupInfo(object sender, SelectionChangedEventArgs e)
        {
            if (Backups.SelectedIndex == 0) return;
            if(File.Exists(exe + "\\BBBUBackups\\" + Backups.SelectedValue + "\\Backup.json"))
            {
                BackupConfig = JSON.Parse(File.ReadAllText(exe + "\\BBBUBackups\\" + Backups.SelectedValue + "\\Backup.json"));
            } else
            {
                BackupConfig = JSON.Parse("{}");
                BackupConfig["BackupType"] = 0;
            }

            if(BackupConfig["BackupType"] == 0)
            {
                RAPK.Visibility = Visibility.Hidden;
                RAPK.IsChecked = false;
            } else
            {
                RAPK.Visibility = Visibility.Visible;
                RAPK.IsChecked = true;
            }
        }

        public void ChangeImage(String ImageName)
        {
            if (MainWindow.config.CustomImage)
            {
                ImageBrush uniformBrush = new ImageBrush();
                uniformBrush.ImageSource = new BitmapImage(new Uri(MainWindow.config.CustomImageSource, UriKind.Absolute));
                uniformBrush.ImageSource = new BitmapImage(new Uri(MainWindow.config.CustomImageSource, UriKind.Absolute));
                uniformBrush.Stretch = Stretch.UniformToFill;
                this.Background = uniformBrush;
            }
            else
            {
                ImageBrush uniformBrush = new ImageBrush();
                uniformBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/" + ImageName, UriKind.Absolute));
                uniformBrush.Stretch = Stretch.UniformToFill;
                this.Background = uniformBrush;
            }
        }
    }
}
