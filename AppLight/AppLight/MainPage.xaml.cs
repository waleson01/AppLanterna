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

    }

    private async void Carrega_Informacoes_bateria()
    {
        try
        {
            if (CrossBattery.IsSupported)
            {
                CrossBattery.Current.BatteryChanged -= Mudanca_Status_Bateria;
                CrossBattery.Current.BatteryChanged += Mudanca_Status_Bateria;
            }
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
            lbl_porcentagem_restante.Text = e.RemainingCharge.ToString() + "%";
            
            if (e.Islow)
            {
                lbl_bateria_fraca = "A bateria está fraca!";
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
            }
        }
    }




}
