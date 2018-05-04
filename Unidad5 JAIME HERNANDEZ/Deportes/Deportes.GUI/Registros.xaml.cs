using Deportes.BIZ;
using Deportes.COMMON.Entidades;
using Deportes.COMMON.Interfaces;
using Deportes.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Deportes.GUI
{
	/// <summary>
	/// Lógica de interacción para Registros.xaml
	/// </summary>
	public partial class Registros : Window
	{
		enum accion
		{
			Nuevo,
			Editar
		}
		IManejadorUsuarios manejadorUsuarios;
		IManejadorDeportes manejadorDeporte;
		IManejadorEquipos manejadorEquipo;
		IManejadorTorneos manejadorTorneo;

		List<Aleatorio> Primerlista = new List<Aleatorio>();
		List<Aleatorio> Segundalista = new List<Aleatorio>();
		List<Torneos> TorneoDeportes = new List<Torneos>();
		string PrimerEquipo;
		string SegundoEquipo;

		accion accionUsuarios;
		accion accionDeporte;
		accion accionEquipo;
		accion accionTorneo;
		public Registros()
		{
			InitializeComponent();

			manejadorUsuarios = new ManejadorUsuario(new RepositorioGenerico<Usuarios>());
			manejadorDeporte = new ManejadorDeporte(new RepositorioGenerico<Deporte>());
			manejadorEquipo = new ManejadorEquipo(new RepositorioGenerico<Equipo>());
			manejadorTorneo = new ManejadorTorneos(new RepositorioGenerico<Torneos>());

			PonerBotonesEquipoEnEdicion(false);
			LimpiarCamposDeEquipo(false);
			ActualizarTablaEquipo();

			PonerBotonesUsuarioEnEdicion(false);
			LimpiarCamposDeUsuario();
			ActualizarTablaUsuario();

			PonerBotonesDeporteEnEdicion(false);
			LimpiarCamposDeDeporte();
			ActualizarTablaDeporte();

			LimpiarCamposDeTorneo(false);
			ActualizarTablaTorneo();

		}

		private void CargarTablas()
		{
			
			dtgPuntuaciones.ItemsSource = null;
			dtgPuntuaciones.ItemsSource = manejadorTorneo.Listar;

			cmbDeportesPuntosEquipos.ItemsSource = null;
			cmbDeportesPuntosEquipos.ItemsSource = manejadorDeporte.Listar;

		}


		private void ActualizarTablaTorneo()
		{
			dtgTorneo.ItemsSource = null;
			dtgTorneo.ItemsSource = manejadorTorneo.Listar;

			

			cmbTDeporte.ItemsSource = null;
			cmbTDeporte.ItemsSource = manejadorDeporte.Listar;
			cmbDeportesPuntosEquipos.ItemsSource = null;
			cmbDeportesPuntosEquipos.ItemsSource = manejadorDeporte.Listar;

		}

		private void LimpiarCamposDeTorneo(bool v)
		{
			cmbTDeporte.ItemsSource = "";
			txbCalendario.Text = "";

			cmbTDeporte.IsEditable = v;
			txbCalendario.IsEnabled = v;
			ActualizarTablaTorneo();

			btnTorneoCancelar.IsEnabled = v;
			btnTorneoEliminar.IsEnabled = !v;
			btnTorneoGuardar.IsEnabled = v;
			btnTorneoNuevo.IsEnabled = !v;
		}

		private void ActualizarTablaEquipo()
		{
			dtgEquipo.ItemsSource = null;
			dtgEquipo.ItemsSource = manejadorEquipo.Listar;



		}

		private void LimpiarCamposDeEquipo(bool value)
		{
			txbEquipo.Clear();
			txbEquipo.IsEnabled = value;
			cmbAreaDeporte.IsEnabled = value;
		}

		private void PonerBotonesEquipoEnEdicion(bool value)
		{
			btnEquipoCancelar.IsEnabled = value;
			btnEquipoEditar.IsEnabled = !value;
			btnEquipoEliminar.IsEnabled = !value;
			btnEquipoGuardar.IsEnabled = value;
			btnEquipoNuevo.IsEnabled = !value;
		}

		private void ActualizarTablaDeporte()
		{
			dtgDeporte.ItemsSource = null;
			dtgDeporte.ItemsSource = manejadorDeporte.Listar;
			cmbAreaDeporte.ItemsSource = manejadorDeporte.Listar;
		}

		private void LimpiarCamposDeDeporte()
		{
			txbTipoDeDeporte.Clear();
		

		}

		private void PonerBotonesDeporteEnEdicion(bool value)
		{
			btnDeporteCancelar.IsEnabled = value;
			btnDeporteEditar.IsEnabled = !value;
			btnDeporteEliminar.IsEnabled = !value;
			btnDeporteGuardar.IsEnabled = value;
			btnDeporteNuevo.IsEnabled = !value;
		}

		private void PonerBotonesUsuarioEnEdicion(bool value)
		{
			btnUsuarioCancelar.IsEnabled = value;
			btnUsuarioEditar.IsEnabled = !value;
			btnUsuarioEliminar.IsEnabled = !value;
			btnUsuarioGuardar.IsEnabled = value;
			btnUsuarioNuevo.IsEnabled = !value;
		}

		private void ActualizarTablaUsuario()
		{
			dtgUsuario.ItemsSource = null;
			dtgUsuario.ItemsSource = manejadorUsuarios.Listar;
		}

		private void LimpiarCamposDeUsuario()
		{
			txbUsuario.Clear();
			txbPasword.Clear();
			txbPasword2.Clear();
		}
			

		

		private void btnUsuarioNuevo_Click(object sender, RoutedEventArgs e)
		{
			LimpiarCamposDeUsuario();
			PonerBotonesUsuarioEnEdicion(true);
			accionUsuarios = accion.Nuevo;

		}

		private void btnUsuarioEditar_Click(object sender, RoutedEventArgs e)
		{
				if (dtgUsuario.SelectedItem != null)
				{

					Usuarios cat = dtgUsuario.SelectedItem as Usuarios;


					txbUsuario.Text = cat.NuevoUsuario;
					txbPasword.Text = cat.Contraseña;
					txbPasword2.Text = cat.ConfirmarContraseña;
					accionUsuarios = accion.Editar;
				    PonerBotonesUsuarioEnEdicion(true);
				}
				else
				{
					MessageBox.Show("No selecciono  ni un elemento de la tabla de los ususarios", "Usuario", MessageBoxButton.OK, MessageBoxImage.Error);
				}
		}

		private void btnUsuarioGuardar_Click(object sender, RoutedEventArgs e)
		{
			if (accionUsuarios == accion.Nuevo)
			{
				Usuarios cat = new Usuarios()
				{
					NuevoUsuario = txbUsuario.Text,
					Contraseña = txbPasword.Text,
					ConfirmarContraseña = txbPasword2.Text
					
				};
				if (manejadorUsuarios.Agregar(cat))
				{
					MessageBox.Show("Usuario agregado correctamente", "Usuario", MessageBoxButton.OK, MessageBoxImage.Information);
					LimpiarCamposDeUsuario();
					ActualizarTablaUsuario();
					PonerBotonesUsuarioEnEdicion(false);
				}
				else
				{
					MessageBox.Show("El Usuario no se pudo agregar", "Usuario", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
			else
			{
				Usuarios cat = dtgUsuario.SelectedItem as Usuarios;
				cat.Contraseña = txbPasword.Text;
				cat.NuevoUsuario = txbUsuario.Text;
				cat.ConfirmarContraseña = txbPasword2.Text;
				
				if (manejadorUsuarios.Modificar(cat))
				{
					MessageBox.Show("Usuario modificado correctamente", "Usuario", MessageBoxButton.OK, MessageBoxImage.Information);
					LimpiarCamposDeUsuario();
					ActualizarTablaUsuario();
					PonerBotonesUsuarioEnEdicion(false);
				}
				else
				{
					MessageBox.Show("El Usuario no se pudo actualizar", "Usuario", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}

		private void btnUsuarioCancelar_Click(object sender, RoutedEventArgs e)
		{
			LimpiarCamposDeUsuario();
			PonerBotonesUsuarioEnEdicion(false);
		}

		private void btnUsuarioEliminar_Click(object sender, RoutedEventArgs e)
		{
			Usuarios cat = dtgUsuario.SelectedItem as Usuarios;
			if (cat != null)
			{
				if (MessageBox.Show("Realmente deseas eliminar este Usuario?", "Usuario", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
				{
					if (manejadorUsuarios.Eliminar(cat.Id))
					{
						MessageBox.Show("Usuario eliminado", "Usuario", MessageBoxButton.OK, MessageBoxImage.Information);
						ActualizarTablaUsuario();
					}
					else
					{
						MessageBox.Show("No se pudo eliminar el Usuario", "Usuario", MessageBoxButton.OK, MessageBoxImage.Information);
					}
				}
			}
		}

		private void btnDeporteNuevo_Click(object sender, RoutedEventArgs e)
		{
			LimpiarCamposDeDeporte();
			PonerBotonesDeporteEnEdicion(true);
			accionDeporte = accion.Nuevo;
		}

		private void btnDeporteEditar_Click(object sender, RoutedEventArgs e)
		{
			Deporte emp = dtgDeporte.SelectedItem as Deporte;
			if (emp != null)
			{
				txbTipoDeDeporte.Text = emp.TipoDeDeporte;
				accionDeporte = accion.Editar;
				PonerBotonesDeporteEnEdicion(true);
			}
		}

		private void btnDeporteGuardar_Click(object sender, RoutedEventArgs e)
		{
			if (accionDeporte == accion.Nuevo)
			{
				Deporte cat = new Deporte()
				{
					TipoDeDeporte = txbTipoDeDeporte.Text

				};
				if (manejadorDeporte.Agregar(cat))
				{
					MessageBox.Show("Deporte agregado correctamente", "Deporte", MessageBoxButton.OK, MessageBoxImage.Information);
					LimpiarCamposDeDeporte();
					ActualizarTablaDeporte();
					PonerBotonesDeporteEnEdicion(false);
				}
				else
				{
					MessageBox.Show("El Deporte no se pudo agregar", "Deporte", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
			else
			{
				Deporte cat = dtgDeporte.SelectedItem as Deporte;
				cat.TipoDeDeporte = txbTipoDeDeporte.Text;

				if (manejadorDeporte.Modificar(cat))
				{
					MessageBox.Show("Deporte modificado correctamente", "Deporte", MessageBoxButton.OK, MessageBoxImage.Information);
					LimpiarCamposDeDeporte();
					ActualizarTablaDeporte();
					PonerBotonesDeporteEnEdicion(false);
				}
				else
				{
					MessageBox.Show("El Deporte no se pudo actualizar", "Deporte", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}

		private void btnDeporteCancelar_Click(object sender, RoutedEventArgs e)
		{
			LimpiarCamposDeDeporte();
			PonerBotonesDeporteEnEdicion(false);
		}

		private void btnDeporteEliminar_Click(object sender, RoutedEventArgs e)
		{
			Deporte cat = dtgDeporte.SelectedItem as Deporte;
			if (cat != null)
			{
				if (MessageBox.Show("Realmente deseas eliminar este Deporte?", "Deporte", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
				{
					if (manejadorDeporte.Eliminar(cat.Id))
					{
						MessageBox.Show("Deporte eliminado", "Deporte", MessageBoxButton.OK, MessageBoxImage.Information);
						ActualizarTablaDeporte();
					}
					else
					{
						MessageBox.Show("No se pudo eliminar el Deporte", "Deporte", MessageBoxButton.OK, MessageBoxImage.Information);
					}
				}
			}
		}

		private void btnEquipoNuevo_Click(object sender, RoutedEventArgs e)
		{
			LimpiarCamposDeEquipo(true);
			PonerBotonesEquipoEnEdicion(true);
			accionEquipo = accion.Nuevo;
		}

		private void btnEquipoEditar_Click(object sender, RoutedEventArgs e)
		{
			Equipo cat = dtgEquipo.SelectedItem as Equipo;
			if (cat != null)
			{
				PonerBotonesEquipoEnEdicion(true);
				LimpiarCamposDeEquipo(true);
				txbEquipo.Text = cat.NombreEquipo;
				cmbAreaDeporte.Text = cat.NDeporte;
				accionEquipo = accion.Editar;

			}
		}

		private void btnEquipoGuardar_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(txbEquipo.Text))
			{
				MessageBox.Show("Faltan Datos", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				return;
			}

			if (accionEquipo == accion.Nuevo)
			{
				Equipo cat = new Equipo();

				cat.NombreEquipo = txbEquipo.Text;
				cat.NDeporte = cmbAreaDeporte.Text;


				if (manejadorEquipo.Agregar(cat))
				{
					MessageBox.Show("Equipo agregado correctamente", "Deportes", MessageBoxButton.OK, MessageBoxImage.Information);
					LimpiarCamposDeEquipo(false);
					ActualizarTablaEquipo();
					PonerBotonesEquipoEnEdicion(false);
				}
				else
				{
					MessageBox.Show("El Equipo no se pudo agregar", "Deportes", MessageBoxButton.OK, MessageBoxImage.Error);

				}
			}
			else
			{
				Equipo cat = dtgEquipo.SelectedItem as Equipo;
				cat.NombreEquipo = txbEquipo.Text;
				cat.NDeporte = cmbAreaDeporte.Text;

				if (manejadorEquipo.Modificar(cat))
				{
					MessageBox.Show("Equipo modificado correctamente", "Deportes", MessageBoxButton.OK, MessageBoxImage.Information);
					LimpiarCamposDeEquipo(false);
					ActualizarTablaEquipo();
					PonerBotonesEquipoEnEdicion(false);
				}
				else
				{
					MessageBox.Show("El Equipo no se pudo actualizar", "Equipo", MessageBoxButton.OK, MessageBoxImage.Error);

				}

			}
		}

		private void btnEquipoCancelar_Click(object sender, RoutedEventArgs e)
		{
			LimpiarCamposDeEquipo(false);
			PonerBotonesEquipoEnEdicion(false);
		}

		private void btnEquipoEliminar_Click(object sender, RoutedEventArgs e)
		{
			Equipo cat = dtgEquipo.SelectedItem as Equipo;
			if (MessageBox.Show("Realmente quieres Eliminar este Equipo?", "Deportes", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
			{
				if (manejadorEquipo.Eliminar(cat.Id))
				{
					MessageBox.Show("Equipo eliminado", "Deportes", MessageBoxButton.OK, MessageBoxImage.Information);
					ActualizarTablaEquipo();
				}
				else
				{
					MessageBox.Show("No se puede eliminar el Equipo", "Deportes", MessageBoxButton.OK, MessageBoxImage.Information);

				}
			}
		}

		private void btnTorneoNuevo_Click(object sender, RoutedEventArgs e)
		{
			LimpiarCamposDeTorneo(true);
			TorneoDeportes = new List<Torneos>();
		}

		public void Equipo1()
		{
			int contador = 0;
			Aleatorio a = new Aleatorio();
			foreach (var item in Primerlista)
			{
				contador++;
				if (contador == 1)
				{
					PrimerEquipo = item.GeneradorAleatorio;
					a.GeneradorAleatorio = item.GeneradorAleatorio;
					Primerlista.Remove(item);
					Segundalista.Add(a);
					break;
				}
			}

		}

		private void Equipo2Impar()
		{
			if (Primerlista.Count >= 1)
			{
				Random r = new Random();
				int val = r.Next(1, Primerlista.Count);
				int contador = 0;

				foreach (var item2 in Primerlista)
				{
					contador++;

					if (contador == val)
					{
						SegundoEquipo = item2.GeneradorAleatorio;
						Primerlista.Remove(item2);
						break;
					}
				}
			}
		}

		private void NumerosImpares(string palabra)
		{
			Equipo1();
			Equipo2Impar();
			Torneos a = new Torneos()
			{
				PrimerEquipo = PrimerEquipo,
				SegundoEquipo = SegundoEquipo,
				Puntacion1 = 0,
				puntuacion2 = 0,
				Deporte = palabra,
				Fecha = txbCalendario.Text,
			};
			TorneoDeportes.Add(a);
			manejadorTorneo.Agregar(a);
			dtgTorneo.ItemsSource = null;
			dtgTorneo.ItemsSource = manejadorTorneo.Listar;
		}



		private void btnTorneoCancelar_Click(object sender, RoutedEventArgs e)
		{
			if (MessageBox.Show("Esta realmente seguro de cancelar la operación?", "Torneo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
			{
				ActualizarTablaTorneo();
				LimpiarCamposDeTorneo(false);
			}
		}

		private void btnTorneoEliminar_Click(object sender, RoutedEventArgs e)
		{
			if (dtgTorneo.SelectedItem != null)
			{
				Torneos a = dtgTorneo.SelectedItem as Torneos;
				if (MessageBox.Show("Esta realmente seguro de eliminar los equipos: " + a.PrimerEquipo + " & " + a.SegundoEquipo, "Torneo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
				{
					if (manejadorTorneo.Eliminar(a.Id))
					{
						MessageBox.Show("Equipos eliminados", "Torneo", MessageBoxButton.OK, MessageBoxImage.Information);
						ActualizarTablaTorneo();
					}
					else
					{
						MessageBox.Show("No se puedo eliminar los quipos", "Torneo", MessageBoxButton.OK, MessageBoxImage.Error);
					}
				}
			}
			else
			{
				MessageBox.Show("No ha seleccionado ningun elemento para eliminar\nSeleccione uno", "Torneo Eliminación", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void btnTorneoGuardar_Click(object sender, RoutedEventArgs e)
		{
			if (cmbTDeporte.SelectedItem == null)
			{
				MessageBox.Show("Error.... Por favor selecciona un deporte!!!", "Torneo", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			string pal = cmbTDeporte.Text;
			if (manejadorEquipo.ContadorDeBuscarEquipo(pal) <= 1)
			{
				MessageBox.Show("No se puede realizar los torneos con un solo equipo\nAgregue más equipos", "Torneo", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			if (string.IsNullOrEmpty(txbCalendario.Text))
			{
				MessageBox.Show("Agregue la fecha programada del torneo", "Torneo", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			foreach (var item in manejadorEquipo.Listar)
			{
				if (item.NDeporte == pal)
				{
					Aleatorio equi = new Aleatorio();
					equi.GeneradorAleatorio = item.NombreEquipo;
					Primerlista.Add(equi);
				}
			}


			if (Primerlista.Count % 2 == 0)
			{
				while (((Primerlista.Count) / 2) > 0)
				{
					NumerosImpares(pal);
				}
			}
			else
			{
				while (((Primerlista.Count) / 2) > 0)
				{
					NumerosImpares(pal);
				}
				ElemntosRestantes(pal);
			}


			LimpiarCamposDeTorneo(false);
		}

		private void ElemntosRestantes(string pal)
		{
			Equipo1();
			if (Primerlista.Count >= 1)
			{
				Random r = new Random();
				int val = r.Next(2, Segundalista.Count);
				int contador = 0;

				foreach (var item2 in Segundalista)
				{
					contador++;

					if (contador == val)
					{
						SegundoEquipo = item2.GeneradorAleatorio;
						break;
					}
				}
			}
			Torneos a = new Torneos()
			{
				PrimerEquipo = PrimerEquipo,
				SegundoEquipo = SegundoEquipo,
				Deporte = pal,
				Puntacion1 = 0,
				puntuacion2 = 0,
				Fecha = txbCalendario.Text,
			};
			TorneoDeportes.Add(a);
			manejadorTorneo.Agregar(a);
			dtgTorneo.ItemsSource = null;
			dtgTorneo.ItemsSource = manejadorTorneo.Listar;

		}

		private void btnPuntosGuardar_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(cmbDeportesPuntosEquipos.Text))
			{
				MessageBox.Show("Seleccione un Deporte", "Puntuaciones", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			if (string.IsNullOrEmpty(clcFechaPuntosEquipos.Text))
			{
				MessageBox.Show("Seleccione la fecha de programación del torneo", "Puntuaciones", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			List<Torneos> tipo = new List<Torneos>();
			if (cmbDeportesPuntosEquipos.SelectedItem != null)
			{
				foreach (var item in manejadorTorneo.Listar)
				{
					if (item.Deporte == cmbDeportesPuntosEquipos.Text && item.Fecha == clcFechaPuntosEquipos.Text)
					{
						Torneos TipoDeporte = new Torneos()
						{
							PrimerEquipo = item.PrimerEquipo,
							SegundoEquipo = item.SegundoEquipo,
							Deporte = item.Deporte,
							Fecha = item.Fecha,
							Id = item.Id,
							Puntacion1 = item.Puntacion1,
							puntuacion2 = item.puntuacion2,
						};
						tipo.Add(TipoDeporte);
					}
				}
				dtgPuntuaciones.ItemsSource = null;
				dtgPuntuaciones.ItemsSource = tipo;
			}
			else
			{
				MessageBox.Show("Algo salio mal :(\nIntentalo de nuevo", "Puntuaciones", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void btnPuntosRegresar_Click(object sender, RoutedEventArgs e)
		{
			dtgPuntuaciones.ItemsSource = null;
			dtgPuntuaciones.ItemsSource = manejadorTorneo.Listar;
		}

		private void btnEditarPuntuacion_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(txtPuntuacionId.Text))
			{
				MessageBox.Show("No ha seleccionado ningun campo de la tabla para actualizar", "Puntos Torneo", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			if (manejadorTorneo.VerificarNumero(txtPuntosPuntuacion1.Text) == 1)
			{
				MessageBox.Show("No se aceptan letras, solo numeros en Marcador 1 ", "Puntos Torneo", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			if (manejadorTorneo.VerificarNumero(txtPuntosPuntuacion2.Text) == 1)
			{
				MessageBox.Show("No se aceptan letras, solo numeros en Marcador 2", "Puntos Torneo", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			int PEquipo = 0;
			int SEquipo = 0;
			if (int.Parse(txtPuntosPuntuacion1.Text) > int.Parse(txtPuntosPuntuacion2.Text))
			{
				PEquipo = 3;
				SEquipo = 1;
			}
			if (int.Parse(txtPuntosPuntuacion1.Text) < int.Parse(txtPuntosPuntuacion2.Text))
			{
				PEquipo = 1;
				SEquipo = 3;
			}
			if (int.Parse(txtPuntosPuntuacion1.Text) == int.Parse(txtPuntosPuntuacion2.Text))
			{
				PEquipo = 2;
				SEquipo = 2;
			}

			foreach (var item in manejadorTorneo.Listar)
			{
				if (item.Id.ToString() == txtPuntuacionId.Text)
				{
					item.PrimerEquipo = txtPuntosPrimerEquipo.Text;
					item.SegundoEquipo = txtPuntosSegundoEquipo.Text;
					item.Puntacion1 = PEquipo;
					item.puntuacion2 = SEquipo;
					if (manejadorTorneo.Modificar(item))
					{
						CargarTablas();
						VaciarPuntosTorneos();
						MessageBox.Show("Torneo editado correctamente", "Torneo", MessageBoxButton.OK, MessageBoxImage.Information);
					}
					else
					{
						MessageBox.Show("No se puedo editar correctamente el Torneo", "Torneo", MessageBoxButton.OK, MessageBoxImage.Information);
					}
				}
			}
		}

		private void VaciarPuntosTorneos()
		{
			txtPuntosPuntuacion1.Clear();
			txtPuntosPuntuacion2.Clear();
			txtPuntuacionId.Clear();
			txtPuntosPrimerEquipo.Clear();
			txtPuntosSegundoEquipo.Clear();
		}

		private void btnEliminarPuntuacionEquipos_Click(object sender, RoutedEventArgs e)
		{
			if (dtgPuntuaciones.SelectedItem != null)
			{
				Torneos a = dtgPuntuaciones.SelectedItem as Torneos;
				if (a != null)
				{
					if (MessageBox.Show("Esta realmente seguro de eliminar los equipos: " + a.PrimerEquipo + " & " + a.SegundoEquipo, "Torneo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
					{
						if (manejadorTorneo.Eliminar(a.Id))
						{
							MessageBox.Show("Equipos eliminados", "Puntos Torneo", MessageBoxButton.OK, MessageBoxImage.Information);
							CargarTablas();
							VaciarPuntosTorneos();
						}
						else
						{
							MessageBox.Show("No se puedo eliminar el los quipos", "Torneo", MessageBoxButton.OK, MessageBoxImage.Error);
						}
					}
				}
				else
				{
					Torneos b = dtgPuntuaciones.SelectedItem as Torneos;
					foreach (var item in manejadorTorneo.Listar)
					{
						if (b.Id == item.Id)
						{
							if (MessageBox.Show("Esta realmente seguro de eliminar los equipos: " + item.PrimerEquipo + " & " + item.SegundoEquipo, "Torneo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
							{
								if (manejadorTorneo.Eliminar(item.Id))
								{
									MessageBox.Show("Equipos eliminados", "Puntos Torneo", MessageBoxButton.OK, MessageBoxImage.Information);
									CargarTablas();
									VaciarPuntosTorneos();
								}
								else
								{
									MessageBox.Show("No se puedo eliminar el los quipos", "Torneo", MessageBoxButton.OK, MessageBoxImage.Error);
								}
							}
						}
					}
				}
			}
			else
			{
				MessageBox.Show("No ha seleccionado ningun elemento de la tabla", "Puntos Torneo", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void btnCancelarPuntoPuntuacion_Click(object sender, RoutedEventArgs e)
		{
			if (MessageBox.Show("Esta realmente seguro de cancelar la operación?", "Puntos Torneo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
			{
				CargarTablas();
				VaciarPuntosTorneos();
			}
		}

		private void dtgPuntuaciones_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (dtgPuntuaciones.SelectedItem != null)
			{
				Torneos a = dtgPuntuaciones.SelectedItem as Torneos;
				if (a != null)
				{
					txtPuntuacionId.Text = a.Id.ToString();
					txtPuntosPrimerEquipo.Text = a.PrimerEquipo;
					txtPuntosSegundoEquipo.Text = a.SegundoEquipo;
					txtPuntosPuntuacion1.Text = a.Puntacion1.ToString();
					txtPuntosPuntuacion2.Text = a.puntuacion2.ToString();
				}
				else
				{
					Torneos b = dtgPuntuaciones.SelectedItem as Torneos;
					foreach (var item in manejadorTorneo.Listar)
					{
						if (b.Id == item.Id)
						{
							txtPuntuacionId.Text = item.Id.ToString();
							txtPuntosPrimerEquipo.Text = item.PrimerEquipo;
							txtPuntosSegundoEquipo.Text = item.SegundoEquipo;
							txtPuntosPuntuacion1.Text = item.Puntacion1.ToString();
							txtPuntosPuntuacion2.Text = item.puntuacion2.ToString();
						}
					}
				}
			}
			else
			{
				MessageBox.Show("No ha seleccionado ningun elemento de la tabla", "Puntos Torneo", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void cmbDeportesPuntosEquipos_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}
	}
}
