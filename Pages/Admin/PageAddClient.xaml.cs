using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Coursework.Classes;
using Coursework.DataApp;
using Coursework.Interfaces;
using Coursework.ValidationsModels;


namespace Coursework.Pages.Admin
{

    public partial class PageAddClient : Page
    {
        private IClientRepository _clientRepository;
        public ClientValidation client;

        public PageAddClient(IClientRepository repository)
        {
            InitializeComponent();
            _clientRepository = repository;
            client = new ClientValidation();
            this.DataContext = client;
        }

        private void ButtonAddClient_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckErrors())
                return;
            Client newClient = new Client()
            {
                firstname = client.FirstName,
                lastname = client.LastName,
                phone = client.PhoneNum
            };
            _clientRepository.AddClient(newClient);
            AdminClass.frameMainStruct.Navigate(new PageMerchandising());
        }

        private bool CheckErrors()
        {
            bool enable = true;
            foreach (var element in StackPanelElemets.Children)
            {
                if (element is TextBox textBox)
                {
                    if (Validation.GetHasError(textBox) || string.IsNullOrEmpty(textBox.Text))
                        enable = false;
                }
            }
            return enable;
        }
    }
}
