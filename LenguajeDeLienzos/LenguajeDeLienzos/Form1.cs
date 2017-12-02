
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LenguajeDeLienzos.Gramatica.irony;
using System.Collections;
using System.IO;
using LenguajeDeLienzos.Dibujo;
namespace LenguajeDeLienzos
{
    public partial class Form1 : Form
    {
        private static string clase=""; // nombre del archivo del proyecto
        public static string clasePrincipal; // se utiliza en sintactico
        private static int x; // nos dice cual el indice de la clase principal que se esta ejecutando.
        public static string auxiliar;
        private string Directorio = "";
        public static ArrayList Raices = new ArrayList();
     
       
        public Form1()
        {
            InitializeComponent();
        }
        //protected override void OnMouseMove(MouseEventArgs e)
        //{
        //    base.OnMouseMove(e);
        //    txtX.Text = e.X.ToString();
        //    txtY.Text = e.Y.ToString();        
        //}

       

        private void Form1_Load(object sender, EventArgs e)
        {
            txtbox.Enabled = false;
            txtbox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void herramientasToolStripMenuItem_Click(object sender, EventArgs e)
        {// BOTON EJECUTAR
            Pintar.AreaDibujo = this.pictureBox1;
            Pintar.inicializar();
            RecorrerArbol.TablaDeSimbolos.Clear();//limpiar tabla de simb.
            auxiliar = ""; // limpiamos
           

            x = tabControl1.SelectedIndex; // nos devuelve el # de pestaña q estamos trabajando actualmente.
           
            RichTextBox caja=(RichTextBox)tabControl1.TabPages[x].Controls[0]; // caja = texto escrito en la clase que estamos ejectando
            TabPage nombreActual = tabControl1.TabPages[x];// obtener que pestaña se esta ejecutando.
            clasePrincipal = nombreActual.Text;
            // MessageBox.Show("la clase principal es "+clasePrincipal);

            //-----------paso 1 -> verificar si se quiere extender de alguna clase ------------------------
            int CantidadClases = sintactico.BuscarExtensiones(caja.Text); // llena tambien CLASES y limpia en cada ejecucion
    
            //---------------devuelve la cantidad de clases que se quiere extender----------------


            if (CantidadClases>0) {// ingresa si existen clases a extender

                for (int i=0; i < CantidadClases;i++) { // recorrer cada clase de la cual se quiere extender.

                    string IdClase = sintactico.CLASES[i].ToString(); // nombre clase
                    auxiliar = IdClase; // auxiliar : utilizado en sintactico
             
                    int IndicePestaña=existe_clase(IdClase); // nos devuelve el valor del tab, donde esta la clase
                    if (IndicePestaña >= 0)
                    {
                        RichTextBox txt = (RichTextBox)tabControl1.TabPages[IndicePestaña].Controls[0];// obtener el texto de la clase extendida
                        // caja.Text = "Ejecutando " + IdClase;
                        sintactico.analizar(txt.Text, "extiende");// crea el arbol de cada clase a extender
                    }
                    else {
                        MessageBox.Show("La clase "+IdClase+" no existe ");
                    }
                    
                    
                }

            }



            bool resultado = sintactico.analizar(caja.Text,"");// crea el arbol PRINCIPAL de ejecucion
          
            if (resultado == true)
            {
                txtbox2.Text = "si";

            }
            else
            {
                txtbox2.Text = "no";
            }
        }// pestaña ejecutar

        private void txtarea_TextChanged(object sender, EventArgs e){//Cambia el texto en el textarea
            txtbox.Items.Clear();
            for (int i=0;i<txtarea.Lines.Length;i++) {
                txtbox.Items.Add(i);
            }
            txtbox.SelectedItems.Add(txtarea.GetLineFromCharIndex(txtarea.SelectionStart));
        }

        private void txtarea_SelectionChanged(object sender, EventArgs e)
        {
            txtbox.SelectedItems.Add(txtarea.GetLineFromCharIndex(txtarea.SelectionStart));
        }

        private void proyectoToolStripMenuItem_Click(object sender, EventArgs e)
        {//-------> pestaña Crear Proyecto.
           
            string value = "";
            if (InputBox("Crear nuevo proyecto", "Nombre del proyecto", ref value) == DialogResult.OK)
            {
                if (value.Equals(""))
                {
                    MessageBox.Show("NO HA SELECCIONADO NOMBRE");
                }
                else {
                    
                    clase = value; // (clase) es nombre del proyecto, y clase principal 

                    Crear_Proyecto(clase); // crea una carpeta en la carpeta (bin)
               //     menuStrip1.Items.Add("textito");
           
                    Crear_Clase(clase); // crea una clase principal con el nombre de la carpeta
                   
                }
            }
           
        }//pestaña crear proyecto
        private void CrearRichtext(string nombreClase) { //agregar richtxtbx al tab recien creado

            foreach (TabPage pagina in tabControl1.TabPages){

                if (pagina.Text.Equals(nombreClase)) {
                    RichTextBox area = new RichTextBox();
                    area.Name = nombreClase;
                    area.Height = 390;
                    area.Width = 467;
                    area.BackColor = Color.LightGray;
                    pagina.Controls.Add(area);
                   

                }
            
            }

        }// crear richtextbox
        private int existe_clase(string nombre_clase)
        { // verifica si el nombre de una clase ya existe, si no existe, crea un tab con el nombre de la clase
            int busqueda =-1;
            int conta = 0;
            foreach (TabPage pagina in tabControl1.TabPages) {
               
                if (pagina.Text.Equals(nombre_clase)){
                  
                    busqueda =conta;
                   // MessageBox.Show("Ya existe una clase con ese nombre y es : "+busqueda);
                }
                conta++;

            }
            return busqueda;
        }// verificar si existe una clase
        public static DialogResult InputBox(string title, string promptText, ref string value)
        { // se ejecuta al crear un archivo.
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        private void reportesToolStripMenuItem_Click(object sender, EventArgs e)
        {// PESTAÑA REPORTES    
            //Pintar.AreaDibujo = this.pictureBox1;
            //Pintar p = new Pintar();



        }// pestaña reportes

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {// cuando exista un cambio de pestaña

           // x = tabControl1.SelectedIndex;
           // MessageBox.Show("cambie de pestaña "+x);

        }//detector para moverse en los tabs.

        private void tablaDeSimbolosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("el tamaño es  " + RecorrerArbol.TablaDeSimbolos.Count);
            sintactico.GenerarTablaDeSimbolosHtml();
        }
        public void Crear_Proyecto(string nombreDirectorio) {
            string ruta = Application.StartupPath + @"\"+nombreDirectorio;
            Directorio = ruta;
            try {

                if (Directory.Exists(ruta)) {
                    MessageBox.Show("Ya existe un proyecto con este nombre ");

                }else {
                    Directory.CreateDirectory(ruta);
                    MessageBox.Show("Se ha creado un nuevo proyecto");
                }
            }
            catch (Exception e) {
                MessageBox.Show("Error al crear directorio "+e.ToString());
            }

        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        public void Crear_Clase(string nombreClase) { // crea una clase lienzo nueva en forma de tab

            if (existe_clase(nombreClase) == -1)
            {// agrega un tab si esta NO(-1) ha sido creada con anterioridad

                tabControl1.TabPages.Add(clase); // crear nuevo tab. tab principal
                CrearRichtext(clase); // agregar richtext al tab.



            }

        }// metodo para crear clase lienzo

        private void claseLienzoToolStripMenuItem_Click(object sender, EventArgs e)
        {// crear Clases
          

            if (clase.Equals("")) {
                MessageBox.Show("Debe crear un proyecto para poder Agregar Clases");

            }else {

                string value = "";
                if (InputBox("Crear nuevo proyecto", "Nombre del proyecto", ref value) == DialogResult.OK)
                {
                    if (value.Equals("")) {
                        MessageBox.Show("El nombre esta vacio");

                    }else {
                        clase = value;
                        Crear_Clase(clase);
                        Crear_Archivo_LZ(clase);
                    }

                }

           }

        }// pestaña donde se crean Clases

        private void Crear_Archivo_LZ(string nombreArchivo) {

            
            string archivo =Directorio +"\\"+ nombreArchivo + ".lz";
        //    MessageBox.Show(archivo);
            try {

                if (File.Exists(archivo)) { // verificar si el archivo ya existe

                    MessageBox.Show("YA EXISTE UNA CLASE CON ESE NOMBRE");

                }else {
                    using (File.Create(archivo)) ;
                       
                }
            }
            catch (Exception e) {
                MessageBox.Show("ocurrio un error al crear la clase. "+ e.ToString());
            }


        }// Cuando se crea una nueva case crea su archivo.lz asociado

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {// pestaña para guardar todo lo que este escrito en la clase a un archivo.lz de su mismo tipo de clase
            x = tabControl1.SelectedIndex; // # de pestaña 

            RichTextBox caja = (RichTextBox)tabControl1.TabPages[x].Controls[0]; // caja = texto escrito en la clase que estamos ejectando
            TabPage nombreActual = tabControl1.TabPages[x];// obtener que pestaña se esta ejecutando.
            
            Guardar_Archivo(nombreActual.Text,caja.Text); 

        }// pestaña guardar

        public void Guardar_Archivo(string nombreClaseLienzo,string texto) {
            string[] cadenas = texto.Split('\n');
           string archivo = Directorio + "\\" + nombreClaseLienzo + ".lz";
            File.WriteAllLines(archivo,cadenas); // escribe todo el texto del richtextbox, en el archivo

        }// metodo para Almacenar texto

        public void GuardarComo(string nombre,string texto) {
            string[] extension = nombre.Split('.');
            if (extension.Length > 1){

                string[] cadenas = texto.Split('\n');
                string archivo = Directorio + "\\" + nombre;
                File.WriteAllLines(archivo, cadenas); // escribe todo el texto del richtextbox, en el archivo


            }
            else {
                MessageBox.Show("EL ARCHIVO NO TIENE EXTENSION (.Ext)");
            }




        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string value = "";

            if (InputBox("Guardar Archivo", "Nombre del archivo.(ext)", ref value) == DialogResult.OK)
            {
                if (value.Equals("")) {
                    MessageBox.Show("No se ha colocado nombre para el archivo");
                } else {
                    x = tabControl1.SelectedIndex; // # de pestaña 

                    RichTextBox caja = (RichTextBox)tabControl1.TabPages[x].Controls[0]; // caja = texto escrito en la clase que estamos ejectando
                    TabPage nombreActual = tabControl1.TabPages[x];// obtener que pestaña se esta ejecutando.

                    GuardarComo(value, caja.Text);


                }


            }
               
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
                txtX.Text = e.X.ToString();
                txtY.Text = e.Y.ToString();   
        }//  Indicador del cursor
    }

  
   
}

