using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Collections;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace AccessLibrary
{
    public partial class FormLibrary : Form
    {
        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Vahap_Dogan\\OneDrive\\Belgeler\\My_Documents\\Projeler\\CSharp\\AccessDataBase\\LibraryDatabase.mdb");
        public FormLibrary()
        {
            InitializeComponent();
        }

        void Listele()
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter("select * from TblBooks", connection);
            dataAdapter.Fill(dt);
            dt.Columns["Id"].ColumnName = "Kitap No";
            dt.Columns["Name"].ColumnName = "Ad";
            dt.Columns["Author"].ColumnName = "Yazar";
            dt.Columns["Type"].ColumnName = "Tür";
            dt.Columns["PageNumber"].ColumnName = "Sayfa Sayısı";
            dt.Columns["Station"].ColumnName = "Durum";
            dataGridView1.DataSource = dt;
        }

        void Temizle()
        {
            lblId.Text = string.Empty;
            tbName.Text = string.Empty;
            tbAuthor.Text = string.Empty;
            maskedPageNumber.Text = string.Empty;
            comboType.SelectedIndex = -1;
            radioSeconhand.Checked = false;
            radioSeconhand.Enabled = false;
        }

        private void FormLibrary_Load(object sender, EventArgs e)
        {
            Listele();

            connection.Open();
            OleDbCommand command = new OleDbCommand("select distinct Type from TblBooks", connection);
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboType.Items.Add(reader["Type"].ToString());
            }

            connection.Close();
        }

        private void radioUnused_CheckedChanged(object sender, EventArgs e)
        {
            if (radioUnused.Checked)
            {
                lblStation.Text = "1";
            }
            else
            {
                lblStation.Text = "0";
            }
        }

        private void radioSeconhand_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSeconhand.Checked)
            {
                lblStation.Text = "1";
            }
            else
            {
                lblStation.Text = "0";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Boş alan kontrolü
                if (string.IsNullOrEmpty(tbName.Text) || string.IsNullOrEmpty(tbAuthor.Text) || string.IsNullOrEmpty(comboType.Text) || string.IsNullOrEmpty(maskedPageNumber.Text) || string.IsNullOrEmpty(lblStation.Text))
                {
                    MessageBox.Show("Lütfen tüm alanları doldurunuz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    // Veritabanı bağlantısını aç
                    connection.Open();

                    // Sorgu komutu ve parametreler
                    OleDbCommand cmd = new OleDbCommand("insert into TblBooks (Name, Author, Type, PageNumber, Station) values (@p1, @p2, @p3, @p4, @p5)", connection);
                    cmd.Parameters.AddWithValue("@p1", tbName.Text);
                    cmd.Parameters.AddWithValue("@p2", tbAuthor.Text);
                    cmd.Parameters.AddWithValue("@p3", comboType.Text);
                    cmd.Parameters.AddWithValue("@p4", Convert.ToInt32(maskedPageNumber.Text));
                    cmd.Parameters.AddWithValue("@p5", lblStation.Text);

                    // Sorguyu çalıştır
                    cmd.ExecuteNonQuery();

                    // Bağlantıyı kapat
                    connection.Close();

                    MessageBox.Show("Kitap eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Listeyi yenile
                    Listele();
                    Temizle();
                }
            }
            catch (Exception ex)
            {
                // Hata mesajı göster
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secili = dataGridView1.SelectedCells[0].RowIndex;
            lblId.Text = dataGridView1.Rows[secili].Cells[0].Value.ToString();
            tbName.Text = dataGridView1.Rows[secili].Cells[1].Value.ToString();
            tbAuthor.Text = dataGridView1.Rows[secili].Cells[2].Value.ToString();
            comboType.Text = dataGridView1.Rows[secili].Cells[3].Value.ToString();
            maskedPageNumber.Text = dataGridView1.Rows[secili].Cells[4].Value.ToString();
            string bookType = dataGridView1.Rows[secili].Cells[5].Value.ToString();
            if (bookType == "True")
            {
                radioUnused.Checked = true;
            }
            else
            {
                radioSeconhand.Checked = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(lblId.Text))
                {
                    MessageBox.Show("Silinecek kitabı seçiniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                connection.Open();
                OleDbCommand cmdDelete = new OleDbCommand("delete from TblBooks where Id = @p1", connection);
                cmdDelete.Parameters.AddWithValue("@p1", lblId.Text);
                cmdDelete.ExecuteNonQuery();

                Listele();
                Temizle();
                MessageBox.Show($"{tbName.Text} kitabı silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Silme sırasında bir hata ile karşılaşıldı : " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(lblId.Text))
                {
                    MessageBox.Show("Güncellenecek kitabı seçiniz", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                connection.Open();
                OleDbCommand cmdEdit = new OleDbCommand("UPDATE TblBooks SET Name = @p1, Author = @p2, Type = @p3, PageNumber = @p4, Station = @p5 where ID = @p6", connection);

                // Parametreleri ekle
                cmdEdit.Parameters.AddWithValue("@p1", tbName.Text);
                cmdEdit.Parameters.AddWithValue("@p2", tbAuthor.Text);
                cmdEdit.Parameters.AddWithValue("@p3", comboType.Text);
                cmdEdit.Parameters.AddWithValue("@p4", maskedPageNumber.Text);
                cmdEdit.Parameters.AddWithValue("@p5", lblStation.Text);
                cmdEdit.Parameters.AddWithValue("@p6", lblId.Text);
                cmdEdit.ExecuteNonQuery();
                MessageBox.Show("Kitap Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information );

                Listele();
                Temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            OleDbCommand cmdSearch = new OleDbCommand("SELECT * FROM TblBooks where Name = '@p1'", connection);
            cmdSearch.Parameters.AddWithValue("@p1", tbSearch.Text);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmdSearch);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            OleDbCommand cmdSearch1 = new OleDbCommand("SELECT * FROM TblBooks WHERE Name LIKE ?", connection);
            cmdSearch1.Parameters.AddWithValue("?", tbSearch.Text + "%");
            DataTable dt1 = new DataTable();
            OleDbDataAdapter da1 = new OleDbDataAdapter(cmdSearch1);
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;
        }
    }
}
