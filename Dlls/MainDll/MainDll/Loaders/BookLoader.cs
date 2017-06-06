using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Lab_4.Books;
using Lab_4.Helpers;
using Lab_4.Helpers.Serialization;
using Lab_4.Helpers.Formatters;

namespace Lab_4.Loaders
{
    public class BookLoader
    {
        public virtual Book Create(GroupBox g)
        {
            Book b = new Book();
            IEnumerable<TextBox> tbList = ((Grid)g.Content).Children.OfType<TextBox>();

            b.Author = tbList.First(x => x.Name == "InpAuthor").Text;
            b.Name = tbList.First(x => x.Name == "InpName").Text;
            b.PublishingOffice = tbList.First(x => x.Name == "InpPublishing").Text;
            return b;
        }

        public virtual Book BaseCreate(GroupBox g)
        {
            return Create(g);
        }

        public virtual Grid Load(Book b)
        {
            Grid g = new Grid();
            g.Children.Add(FormCreator.CreateLabel("Author", new Thickness(10, 27, 0, 0)));
            g.Children.Add(FormCreator.CreateTextBox("InpAuthor", b.Author, new Thickness(10, 55, 0, 0)));
            g.Children.Add(FormCreator.CreateLabel("Name", new Thickness(10, 77, 0, 0)));
            g.Children.Add(FormCreator.CreateTextBox("InpName", b.Name, new Thickness(10, 105, 0, 0)));
            g.Children.Add(FormCreator.CreateLabel("Publishing office", new Thickness(10, 128, 0, 0)));
            g.Children.Add(FormCreator.CreateTextBox("InpPublishing", b.PublishingOffice, new Thickness(10, 156, 0, 0)));
            g.Children.Add(FormCreator.CreateLabel("Book genre", new Thickness(10, 183, 0, 0)));

            ComboBox cb = FormCreator.CreateComboBox("ChooseGenre", new Thickness(10, 211, 0, 0), LoaderManager.GetChildren("Book"));
            cb.SelectionChanged += new SelectionChangedEventHandler(SelectionChanged);
            g.Children.Add(cb);

            Button btn = FormCreator.CreateButton("BtnLoadPlugin", "Load plugin", new Thickness(10, 330, 0, 0), BtnLoadPlugin_Click);
            btn.Width = 134;
            g.Children.Add(btn);

            return g;
        }

        public GroupBox CreateButtonsGroup()
        {
            Grid g = FormCreator.CreateGrid(new Thickness(0, 0, 0, 0));

            g.Children.Add(FormCreator.CreateButton("BtnAdd", "Add", new Thickness(10, 0, 0, 0), BtnAdd_Click));
            g.Children.Add(FormCreator.CreateButton("BtnRemove", "Remove", new Thickness(75, 0, 0, 0), BtnRemove_Click));
            g.Children.Add(FormCreator.CreateButton("BtnSubmit", "Submit", new Thickness(140, 0, 0, 0), BtnSubmit_Click));
            g.Children.Add(FormCreator.CreateButton("BtnSerialize", "Serialize", new Thickness(205, 0, 0, 0), BtnSerialize_Click));
            g.Children.Add(FormCreator.CreateButton("BtnDeserialize", "Deserialize", new Thickness(270, 0, 0, 0), BtnDeserialize_Click));

            GroupBox gb = FormCreator.CreateGroupBox("ButtonGroup", "", new Thickness(520, 0, 0, 0), 352, 362);
            gb.Content = g;

            return gb;
        }

        private GroupBox GetMainGroupBox(object o)
        {
            FrameworkElement parent = (FrameworkElement)((FrameworkElement)o).Parent;
            if (parent.Name == "MainGroup") return (GroupBox)parent;                 // found MainGroupBox
            else return GetMainGroupBox(parent);
        }

        private void AddMainMenu(object sender)
        {
            GroupBox oldGroupBox = GetMainGroupBox(sender);
            Grid g = (Grid)oldGroupBox.Parent;
            Menu menu = GetMenu(g);
            if (menu != null)
            {
                IEnumerable<Menu> menuList = g.Children.OfType<Menu>();
                if (menuList.Count() != 0)
                {
                    Menu prevMenu = menuList.First(x => x.Name == "Formattions");
                    g.Children.Remove(prevMenu);
                }
                g.Children.Add(menu);
            }
        }

        public Menu GetMenu(Grid g)
        {
            if (FormatterManager.GetFormatters().Count != 0)
            {
                return FormatterManager.GetMenu();
            }
            else return null;
        }

        // EVENTS

        public void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            GroupBox gr = GetMainGroupBox(sender);                  // MainGroupBox
            Grid g = (Grid)gr.Parent;                               // MainGrid

            Book book = Create(gr);                              // create new book based on layout

            var temp = ((Grid)gr.Content).Children;                 // get all children of MainGroupBox
            string type;
            try
            {
                type = ((GroupBox)temp[temp.Count - 2]).Header.ToString();   // get pre-last GroupBox Header, because last one is ButtonGroupBox
            }
            catch { type = "Book"; }

            ListView bookListForm = g.Children.OfType<ListView>().First(x => x.Name == "BookListForm"); // find BookListForm
            bookListForm.Items.Add(new ItemInList { Type = type, Name = book.Name, Author = book.Author, Data = book });
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            GroupBox gr = GetMainGroupBox(sender);                  // MainGroupBox
            Grid g = (Grid)gr.Parent;                               // MainGrid
            ListView bookListForm = g.Children.OfType<ListView>().First(x => x.Name == "BookListForm"); // find BookListForm

            while (bookListForm.SelectedItems.Count > 0)
            {
                bookListForm.Items.Remove(bookListForm.SelectedItems[0]);
            }
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            GroupBox gr = GetMainGroupBox(sender);
            Grid g = (Grid)gr.Parent;

            Book book = Create(gr);

            var temp = ((Grid)gr.Content).Children;
            string type;
            try { type = ((GroupBox)temp[temp.Count - 2]).Header.ToString(); } catch { type = "Book"; }

            ListView bookListForm = g.Children.OfType<ListView>().First(x => x.Name == "BookListForm");
            bookListForm.Items[bookListForm.SelectedIndex] = new ItemInList { Type = type, Name = book.Name, Author = book.Author, Data = book };
        }       // like BtnAdd_Click + get selected and replace with book

        private void BtnSerialize_Click(object sender, RoutedEventArgs e)
        {
            GroupBox gr = GetMainGroupBox(sender);
            Grid g = (Grid)gr.Parent;
            ListView bookListForm = g.Children.OfType<ListView>().First(x => x.Name == "BookListForm");

            SaveFileDialog dlg = new SaveFileDialog() { FileName = "bookList.json" };
            SerializerManager manager = SerializerManager.GetInstance;
            foreach (string item in manager.GetAvailableFormats())
            {
                try { dlg.Filter += item.ToUpper() + " files | *." + item; }
                catch { dlg.Filter += "| " + item.ToUpper() + " files | *." + item; }
            }

            if (dlg.ShowDialog() == true)
            {
                TextWriter writer = new StringWriter();
                
                ISerializer serializer = manager.GetSerializer((Path.GetExtension(dlg.FileName)).Substring(1));
                foreach (ItemInList item in bookListForm.SelectedItems) { writer.WriteLine(" : " + item.Type + " : " + serializer.Serialize(item.Data)); }

                try
                {
                    Menu menu = new Menu();
                    menu = g.Children.OfType<Menu>().First(x => x.Name == "Formattions");
                    string errors = "";
                    foreach (MenuItem item in menu.Items)
                    {
                        MenuItem subItem = (MenuItem)item.Items[0];
                        IFormatter formatter = FormatterManager.GetByKey(item.Name);
                        if (subItem.IsChecked)
                        {
                            if (formatter.IsCompatible(Path.GetExtension(dlg.FileName)))
                            {
                                StreamWriter stream = new StreamWriter(dlg.OpenFile());
                                try
                                {
                                    string[] words = Regex.Split(writer.ToString(), " : ");
                                    for (int i = 2; i < words.Count(); i += 2)
                                    {
                                        stream.WriteLine(" : " + words[i - 1] + " : " + formatter.Format(words[i]));
                                    }
                                }
                                finally { stream.Dispose(); stream.Close(); }
                            }
                            else errors += item.Name + "\n";
                        }
                    }
                    if (errors != "") MessageBox.Show("Incompatible type for:\n" + errors, "Oups!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                    StreamWriter stream = new StreamWriter(dlg.OpenFile());
                    stream.Write(writer);
                    stream.Dispose();
                    stream.Close();
                }
                writer.Dispose();
                writer.Close();
            }
        }    // selectedItems (data & type), serialize { type: "Type", book: Object }, write in file

        private void BtnDeserialize_Click(object sender, RoutedEventArgs e)
        {
            GroupBox gr = GetMainGroupBox(sender);
            Grid g = (Grid)gr.Parent;
            ListView bookListForm = g.Children.OfType<ListView>().First(x => x.Name == "BookListForm");

            OpenFileDialog dlg = new OpenFileDialog();

            SerializerManager manager = SerializerManager.GetInstance;
            foreach (string item in manager.GetAvailableFormats())
            {
                try { dlg.Filter += item.ToUpper() + " files | *." + item; }
                catch { dlg.Filter += "| " + item.ToUpper() + " files | *." + item; }
            }

            if (dlg.ShowDialog() == true)
            {
                StreamReader reader = new StreamReader(dlg.OpenFile());
                ISerializer serializer = manager.GetSerializer((Path.GetExtension(dlg.FileName)).Substring(1));
                string item;
                string loadingErrors = "";
                try
                {
                    item = reader.ReadToEnd();
                    string[] words = Regex.Split(item, " : ");
                    try
                    {
                        Menu menu = new Menu();
                        menu = g.Children.OfType<Menu>().First(x => x.Name == "Formattions");

                        reader.Dispose();
                        reader.Close();
                        foreach (MenuItem smth in menu.Items)
                        {
                            MenuItem subItem = (MenuItem)smth.Items[0];
                            IFormatter formatter = FormatterManager.GetByKey(smth.Name);
                            if (subItem.IsChecked)
                            {
                                if (formatter.IsCompatible(Path.GetExtension(dlg.FileName)))
                                {
                                    TextWriter stream = new StringWriter();
                                    try
                                    {
                                        for (int i = 2; i < words.Count(); i += 2)
                                        {
                                            stream.WriteLine(" : " + words[i - 1] + " : " + formatter.ReFormat(words[i]));
                                        }
                                        words = Regex.Split(stream.ToString(), " : ");
                                    }
                                    finally { stream.Dispose(); stream.Close(); }
                                }
                            }
                        }
                    }
                    catch { }
                    finally
                    {
                        for (int i = 1; i < words.Count(); i += 2)
                        {
                            item = words[i + 1];
                            try
                            {
                                var loader = LoaderManager.GetLoader(words[i]);
                                Book book = loader.Deserialize(item, serializer);
                                bookListForm.Items.Add(new ItemInList { Type = words[i], Name = book.Name, Author = book.Author, Data = book });
                            }
                            catch
                            {
                                loadingErrors += words[i] + "\n";
                            }
                        }
                        if (loadingErrors != "")
                        {
                            MessageBox.Show(loadingErrors, "Unknown types were not serializated:", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }                    
                }
                catch { MessageBox.Show("Smth wrong with loaded file. Please, try again.", "Oups!", MessageBoxButton.OK, MessageBoxImage.Error); }                
                reader.Dispose();
                reader.Close();
            }
        }

        public static List<Type> GetTypes<T>(Assembly assembly)
        {
            if (!typeof(T).IsInterface)
                return null;

            return assembly.GetTypes()
                .Where(x => x.GetInterface(typeof(T).Name) != null)
                .ToList();
        }

        private byte[] GetHash(string path)
        {
            FileStream stream = File.OpenRead(path);
            SHA256Managed sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(stream);
            return hash;
        }

        public bool CheckPluginSignature(string path)
        {
            /*try
            {
                StreamReader reader = new StreamReader(Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + ".mys");
                ISerializer serializer = SerializerManager.GetSerializer("json");
                Structure signature = serializer.Deserialize<Structure>(reader.ReadLine());
                reader.Dispose();
                reader = new StreamReader(path);
                Structure plugin = new Structure()
                {
                    Date = File.GetCreationTime(path),
                    Hash = GetHash(path)
                };
                reader.Dispose();
                reader.Close();

                if ((signature.Date == plugin.Date) && (Enumerable.SequenceEqual(signature.Hash, plugin.Hash))) return true;
                else return false;
            }
            catch
            {
                return false;
            }*/
            return true;
        }

        private void LoadSerializerPlugin(OpenFileDialog dlg)
        {
            Assembly mainAssembly = Assembly.LoadFrom(dlg.FileName);
            List<Type> pluginTypes = GetTypes<IFormatPlugin>(mainAssembly);
            if (pluginTypes.Count != 0)
            {
                SerializerManager manager = SerializerManager.GetInstance;
                foreach (Type item in pluginTypes)
                {
                    IFormatPlugin plugin = Activator.CreateInstance(item) as IFormatPlugin;
                    manager.LoadFormat(plugin.GetName(), plugin.GetSerializer());
                }
            }
        }

        private void LoadBookPlugin(OpenFileDialog dlg)
        {
            Assembly mainAssembly = Assembly.LoadFrom(dlg.FileName);
            List<Type> pluginTypes = GetTypes<IPlugin>(mainAssembly);
            if (pluginTypes.Count != 0)
            {
                foreach (Type item in pluginTypes)
                {
                    IPlugin plugin = Activator.CreateInstance(item) as IPlugin;
                    LoaderManager.AddLoader(plugin.GetName(), plugin.GetParent(), plugin.GetHierarchy());
                }
            }
        }

        private void LoadFormatterPlugin(OpenFileDialog dlg, object sender)
        {
            GroupBox gr = GetMainGroupBox(sender);
            Grid g = (Grid)gr.Parent;
            
            Assembly mainAssembly = Assembly.LoadFrom(dlg.FileName);
            List<Type> pluginTypes = GetTypes<IFormatterPlugin>(mainAssembly);

            if (pluginTypes.Count != 0)
            {
                foreach (Type item in pluginTypes)
                {
                    IFormatterPlugin plugin = Activator.CreateInstance(item) as IFormatterPlugin;
                    FormatterManager.AddFormatter(plugin.GetFunc(), plugin.GetFormatter(), plugin.GetMenuItem());
                }
            }
        }

        private void BtnLoadPlugin_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog() { Filter = "DLL files | *.dll" };
            if (dlg.ShowDialog() == true)
            {
                if (CheckPluginSignature(dlg.FileName))
                {
                    if ((dlg.FileName).Contains("Serializer")) { LoadSerializerPlugin(dlg); }
                    else
                        if ((dlg.FileName).Contains("Formatter") || (dlg.FileName).Contains("Adapter")) { LoadFormatterPlugin(dlg, sender); }
                        else
                            try { LoadBookPlugin(dlg); }
                            catch { MessageBox.Show("Correct plugin? 're you shure?", "Smth wrong!", MessageBoxButton.OK, MessageBoxImage.Error); }

                    GroupBox gr = GetMainGroupBox(sender);                  // MainGroupBox
                    Grid g = (Grid)gr.Parent;                               // MainGrid

                    Book book = Create(gr);                              // create new book based on layout

                    var temp = ((Grid)gr.Content).Children;                 // get all children of MainGroupBox
                    string type;
                    try { type = ((GroupBox)temp[temp.Count - 2]).Header.ToString(); }  // get pre-last GroupBox Header, because last one is ButtonGroupBox
                    catch { type = "Book"; }

                    BookLoader loader = LoaderManager.GetLoader(type);
                    loader.Load(book);
                    AddMainMenu(sender);
                }
                else
                {
                    MessageBox.Show("It's not our official plugin =_=\n\nOr something wrong with signature-file. Sorry :(", "What are you doing?!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        protected void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBox)sender).IsDropDownOpen)
            {
                string selectedText = ((ComboBox)sender).SelectedValue.ToString();

                GroupBox oldGroupBox = GetMainGroupBox(sender);     // MainGroupBox
                Grid p = (Grid)oldGroupBox.Parent;                  // MainGrid
                p.Children.Remove(oldGroupBox);                     // delete old MainGroupBox

                var b = LoaderManager.GetLoader(selectedText);      // select Loader

                Grid newGrid = b.Load(b.BaseCreate(oldGroupBox));   // create new Grid
                AddMainMenu(sender);
                newGrid.Children.Add(b.CreateButtonsGroup());         // add buttons on it

                GroupBox newGroupBox = FormCreator.CreateGroupBox("MainGroup", "Book", new Thickness(0, 0, 0, 0), 887, 384);
                newGroupBox.Content = newGrid;                      // wrap Grid into new MainGroupBox

                p.Children.Add(newGroupBox);                        // add to MainGrid
            }
        }

        public virtual Book Deserialize(string d, ISerializer serializer)
        {
            return serializer.Deserialize<Book>(d);
        }
    }
}
