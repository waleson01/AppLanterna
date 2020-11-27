using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Xamarin.Essentials;
using Plugin.Battery;

namespace AppLight
{
    public partial class MainPage : ContentPage
    {
        bool lanterna_ligada = false;
        public MainPage()
        {
            InitializeComponent();

            btnOnOff.Source = ImageSource.FromResource("AppLight.Images.botao-desligado.jpg");

            Carrega_Informacoes_Bateria();
        }

    

         private async void Carrega_Informacoes_Bateria()
          {
             try
             {
                if (CrossBattery.IsSupported)
                {​​​​
                    CrossBattery.Current.BatteryChanged -= Mudanca_Status_Bateria;
                    CrossBattery.Current.BatteryChanged += Mudanca_Status_Bateria;
                }​​​​
                else
                {
                    lbl_bateria_fraca.Text = "As informações sobre a bateria não está disponível :( ";
                }
             }
            catch (Exception ex)
            {
                await DisplayAlert("Ocorreu um erro: \n", ex.Message, "OK!");
            }
          }

    private async void Mudanca_Status_Bateria (object sender, Plugin.Battery.Abstractions.BatteryChangedEventsAgrs e)
    {
        try
        {
            lbl_porcentagem_restante.Text = e.RemainingChargePercent.ToString() + "%";
            
            if (e.Islow)
            {
                lbl_bateria_fraca.Text = "A bateria está fraca!";
            }
            else
            {
                lbl_bateria_fraca.Text = "";
            }
            switch(e.Status)
                {
                case Plugin.Battery.Abstractions.BatteryStatus.Charging:
                    lbl_status.Text = "Carregando";
                    break;

                case Plugin.Battery.Abstractions.BatteryStatus.Discharging:
                    lbl_status.Text = "Descarregando";
                    break;

                case Plugin.Battery.Abstractions.BatteryStatus.Full:
                    lbl_status.Text = "Bateria Cheia";
                    break;

                case Plugin.Battery.Abstractions.BatteryStatus.NotCharging:
                    lbl_status.Text = "Não Carregando";
                    break;

                case Plugin.Battery.Abstractions.BatteryStatus.Unknown:
                    lbl_status.Text = "Desconhecido";
                    break;
            }

            switch (e.PowerSource)
            {
                case Plugin.Battery.Abstractions.PowerSource.Ac:
                    lbl_fonte_carregamento.Text = "Carregador";
                    break;

                case Plugin.Battery.Abstractions.PowerSource.Battery:
                    lbl_fonte_carregamento.Text = "Bateria";
                    break;

                case Plugin.Battery.Abstractions.PowerSource.Usb:
                    lbl_fonte_carregamento.Text = "USB";
                    break;

                case Plugin.Battery.Abstractions.PowerSource.Wireless:
                    lbl_fonte_carregamento.Text = "Sem Fio";
                    break;

                case Plugin.Battery.Abstractions.PowerSource.Other:
                    lbl_fonte_carregamento.Text = "Desconhecida";
                    break;
            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("Ocorreu um erro: \n ", ex.Message, "OK");
        }
    }

    private async void btnOnOff_Clicked (object sender, EventArgs e)
    {
        try
        {
            if (!laterna_ligada)
            {
                laterna_ligada = true;

                btnOnOff.Source = ImageSource.FromResource("AppLigth.Images.botao-ligado.jpg");
                Vibration.Vibrate(TimeSpan.FromMilliseconds(250));
                await Flashlight.TurnOnAsync();
            }
            else
            {
                lanterna_ligada = false;
                btnOnOff.Source = ImageSource.FromResource("AppLight.Images.botao-desligado.jpg");
                Vibration.Vibrate(TimeSpan.FromMilliseconds(250));
                await Flashlight.TurnOffAsync();
            }
        }

        catch (Exception ex)
        {
            await DisplayAlert("Ocorreu um erro: \n ", ex.Message, "OK");
        }
    }




    }
}
