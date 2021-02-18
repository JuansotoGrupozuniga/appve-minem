﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sres.da;
using sres.be;
using System.Data;
using Oracle.DataAccess.Client;
using sres.ut;

namespace sres.ln
{
    public class ElectromovilidadLN : BaseLN
    {
        ElectromivilidadDA elecDA = new ElectromivilidadDA();

        public FactorDataBE ListaFactor1P(int idfactor, int p1, int v1)
        {
            FactorDataBE obj = new FactorDataBE();
            try
            {
                cn.Open();
                obj = elecDA.ListaFactor1P(idfactor, p1, v1, cn);
            }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }

            return obj;
        }

        public FactorDataBE ListaFactor2P(int idfactor, int p1, int p2, int v1, int v2)
        {
            FactorDataBE obj = new FactorDataBE();
            try
            {
                cn.Open();
                obj = elecDA.ListaFactor2P(idfactor, p1, p2, v1, v2, cn);
            }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }

            return obj;
        }

        public FactorDataBE ListaFactor3P(int idfactor, int p1, int p2, int p3, int v1, int v2, int v3)
        {
            FactorDataBE obj = new FactorDataBE();
            try
            {
                cn.Open();
                obj = elecDA.ListaFactor3P(idfactor, p1, p2, p3, v1, v2, v3, cn);
            }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }

            return obj;
        }

        public List<EscenarioConvencionalBE> CalcularVC(VehiculoConvencionalBE entidad)
        {
            List<EscenarioConvencionalBE> lista = new List<EscenarioConvencionalBE>();
            try
            {
                decimal ipc = Convert.ToDecimal(AppSettings.Get<string>("dato.ipc"));
                decimal reduccion_eficiencia_motor = Convert.ToDecimal(AppSettings.Get<string>("dato.reduccioneficienciamotor"));
                decimal porc_20kmvc = Convert.ToDecimal(AppSettings.Get<string>("dato.porcentaje20kmvc"));
                decimal porc_100kmvc = Convert.ToDecimal(AppSettings.Get<string>("dato.porcentaje100kmvc"));
                decimal mante_extraordinario = Convert.ToDecimal(AppSettings.Get<string>("dato.mantenimientoextraordinario"));
                decimal tasa_descuento = Convert.ToDecimal(AppSettings.Get<string>("dato.tasadescuento"));

                //Porcentaje depreciacion
                decimal dep02 = Convert.ToDecimal(AppSettings.Get<string>("dato.depreciacion02"));
                decimal dep03 = Convert.ToDecimal(AppSettings.Get<string>("dato.depreciacion03"));
                decimal dep04 = Convert.ToDecimal(AppSettings.Get<string>("dato.depreciacion04"));
                decimal dep05 = Convert.ToDecimal(AppSettings.Get<string>("dato.depreciacion05"));
                decimal dep06 = Convert.ToDecimal(AppSettings.Get<string>("dato.depreciacion06"));
                decimal dep07 = Convert.ToDecimal(AppSettings.Get<string>("dato.depreciacion07"));
                decimal dep08 = Convert.ToDecimal(AppSettings.Get<string>("dato.depreciacion08"));
                decimal dep09 = Convert.ToDecimal(AppSettings.Get<string>("dato.depreciacion09"));
                decimal dep10 = Convert.ToDecimal(AppSettings.Get<string>("dato.depreciacion10"));
                decimal dep11 = Convert.ToDecimal(AppSettings.Get<string>("dato.depreciacion11"));
                decimal dep12 = Convert.ToDecimal(AppSettings.Get<string>("dato.depreciacion12"));
                decimal dep13 = Convert.ToDecimal(AppSettings.Get<string>("dato.depreciacion13"));
                decimal dep14 = Convert.ToDecimal(AppSettings.Get<string>("dato.depreciacion14"));
                decimal dep15 = Convert.ToDecimal(AppSettings.Get<string>("dato.depreciacion15"));

                decimal[] arrCargaFinancieraNominalVC = new decimal[15];
                decimal[] arrInversionInicialNominalVC = new decimal[15];
                decimal[] arrSeguroNominalVC = new decimal[15];
                decimal[] arrEnergiaNominalVC = new decimal[15];
                decimal[] arrManteContinuoNominalVC = new decimal[15];
                decimal[] depreciacionVC = new decimal[15];
                decimal[] arrReventaNominalVC = new decimal[15];
                decimal[] arrManteExtraoNominalVC = new decimal[15];
                decimal[] arrServicioPublicoNominalVC = new decimal[15];

                decimal[] arrCargaFinancieraNetoVC = new decimal[15];
                decimal[] arrInversionInicialNetoVC = new decimal[15];
                decimal[] arrSeguroNetoVC = new decimal[15];
                decimal[] arrEnergiaNetoVC = new decimal[15];
                decimal[] arrManteContinuoNetoVC = new decimal[15];
                decimal[] arrReventaNetoVC = new decimal[15];
                decimal[] arrManteExtraoNetoVC = new decimal[15];
                decimal[] arrServicioPublicoNetoVC = new decimal[15];

                decimal[] arrCargaFinancieraAcumuladoVC = new decimal[15];
                decimal[] arrInversionInicialAcumuladoVC = new decimal[15];
                decimal[] arrSeguroAcumuladoVC = new decimal[15];
                decimal[] arrEnergiaAcumuladoVC = new decimal[15];
                decimal[] arrManteContinuoAcumuladoVC = new decimal[15];
                decimal[] arrReventaAcumuladoVC = new decimal[15];
                decimal[] arrManteExtraoAcumuladoVC = new decimal[15];
                decimal[] arrServicioPublicoAcumuladoVC = new decimal[15];

                //Cuota inicial y carga financiera Nominal VC                
                decimal cuota_inicial = 0;
                if (entidad.TIPO_COMPRA_VC == 1)
                {
                    cuota_inicial = entidad.COSTO_VEHICULO_VC * entidad.PORC_CUOTA_INICIAL_VC;
                    decimal primera_carga = (entidad.COSTO_VEHICULO_VC - cuota_inicial) * (1 + entidad.TASA_INTERES_VC);
                    for (int i = 0; i < entidad.ANIO_CREDITO_VC; i++)
                    {
                        if (i == 0) arrCargaFinancieraNominalVC[i] = primera_carga / entidad.ANIO_CREDITO_VC;
                        else arrCargaFinancieraNominalVC[i] = (primera_carga / entidad.ANIO_CREDITO_VC) * (1 + ipc);
                    }
                }
                else if (entidad.TIPO_COMPRA_VC == 2)
                {
                    cuota_inicial = entidad.COSTO_VEHICULO_VC;
                }
                arrInversionInicialNominalVC[0] = cuota_inicial;

                //Cuota inicial Neto VC  
                arrInversionInicialNetoVC[0] = arrInversionInicialNominalVC[0] / Convert.ToDecimal(Math.Pow(Convert.ToDouble(1 + tasa_descuento), -1 + 1));

                //Cuota inicial Acumulado VC 
                for (var i = 0; i < 15; i++)
                {
                    if (i == 0) arrInversionInicialAcumuladoVC[i] = arrInversionInicialNetoVC[i];
                    else arrInversionInicialAcumuladoVC[i] = arrInversionInicialNetoVC[i] + arrInversionInicialAcumuladoVC[i - 1];
                }

                //carga financiera Neto VC
                for (var i = 0; i < entidad.ANIO_CREDITO_VC; i++)
                {
                    arrCargaFinancieraNetoVC[i] = arrCargaFinancieraNominalVC[i] / Convert.ToDecimal(Math.Pow(Convert.ToDouble(1 + tasa_descuento), -1 + (i + 1)));
                }

                //carga financiera Acumulado VC
                for (var i = 0; i < 15; i++)
                {
                    if (i == 0) arrCargaFinancieraAcumuladoVC[i] = arrCargaFinancieraNetoVC[i];
                    else arrCargaFinancieraAcumuladoVC[i] = arrCargaFinancieraNetoVC[i] + arrCargaFinancieraAcumuladoVC[i - 1];
                }

                //Seguro Nominal VC
                if (entidad.P2 == "1")
                {
                    if (entidad.P_SEGURO_VC == "1")
                    {
                        for (int i = 0; i < 15; i++)
                        {
                            if (i == 0) arrSeguroNominalVC[i] = entidad.SEGURO_VC;
                            else arrSeguroNominalVC[i] = arrSeguroNominalVC[i - 1] * (1 + ipc);
                        }
                    }
                }
                else if (entidad.P1 == "1")
                {
                    for (int i = 0; i < 15; i++)
                    {
                        if (i == 0) arrSeguroNominalVC[i] = entidad.SEGURO_VC;
                        else arrSeguroNominalVC[i] = arrSeguroNominalVC[i - 1] * (1 + ipc);
                    }
                }                

                //Seguro Neto VC
                for (var i = 0; i < 15; i++)
                {
                    arrSeguroNetoVC[i] = arrSeguroNominalVC[i] / Convert.ToDecimal(Math.Pow(Convert.ToDouble(1 + tasa_descuento), -1 + (i + 1)));
                }

                //Seguro Acumulado VC
                for (var i = 0; i < 15; i++)
                {
                    if (i == 0) arrSeguroAcumuladoVC[i] = arrSeguroNetoVC[i];
                    else arrSeguroAcumuladoVC[i] = arrSeguroNetoVC[i] + arrSeguroAcumuladoVC[i - 1];
                }

                //Energia (Electricidad y combustible) Nominal VC
                if (entidad.P2 == "1" || entidad.P1 == "1")
                {
                    if (entidad.P_GASTO_COMBUSTIBLE == "1")
                    {
                        for (int i = 0; i < 15; i++)
                        {
                            if (i == 0) arrEnergiaNominalVC[i] = entidad.GASTO_COMBUSTIBLE_VC * 4 * entidad.MESES_USO_VC;
                            else arrEnergiaNominalVC[i] = arrEnergiaNominalVC[i - 1] * (1 + ipc) * (1 + reduccion_eficiencia_motor) * (1 + entidad.PORC_AUMENTO_COMBUSTIBLE_VC);
                        }
                    }
                    else
                    {
                        decimal km_anual = (entidad.KILOMETRO_SEMANAL_VC * 52) * (entidad.MESES_USO_VC / 12);
                        for (int i = 0; i < 15; i++)
                        {
                            if (i == 0) arrEnergiaNominalVC[i] = (km_anual / entidad.RENDIMIENTO_VC) * entidad.PRECIO_COMBUSTIBLE_VC;
                            else arrEnergiaNominalVC[i] = arrEnergiaNominalVC[i - 1] * (1 + ipc) * (1 + reduccion_eficiencia_motor) * (1 + entidad.PORC_AUMENTO_COMBUSTIBLE_VC);
                        }
                    }
                }                

                //Energia (Electricidad y combustible) Neto VC
                for (var i = 0; i < 15; i++)
                {
                    arrEnergiaNetoVC[i] = arrEnergiaNominalVC[i] / Convert.ToDecimal(Math.Pow(Convert.ToDouble(1 + tasa_descuento), -1 + (i + 1)));
                }

                //Energia (Electricidad y combustible) Acumulado VC
                for (var i = 0; i < 15; i++)
                {
                    if (i == 0) arrEnergiaAcumuladoVC[i] = arrEnergiaNetoVC[i];
                    else arrEnergiaAcumuladoVC[i] = arrEnergiaNetoVC[i] + arrEnergiaAcumuladoVC[i - 1];
                }

                //Mantenimiento continuo Nominal VC
                if (entidad.P2 == "1")
                {
                    decimal mantenim_20kmvc = entidad.COSTO_VEHICULO_VC * porc_20kmvc;
                    decimal mantenim_100kmvc = entidad.COSTO_VEHICULO_VC * porc_100kmvc;
                    for (int i = 0; i < 15; i++)
                    {
                        if (i == 0) arrManteContinuoNominalVC[i] = mantenim_20kmvc;
                        else if (i == 1) arrManteContinuoNominalVC[i] = mantenim_100kmvc;
                        else arrManteContinuoNominalVC[i] = arrManteContinuoNominalVC[i - 1] * (1 + ipc);
                    }
                }
                else if (entidad.P1 == "1")
                {
                    for (int i = 0; i < 15; i++)
                    {
                        if (i == 0) arrManteContinuoNominalVC[i] = entidad.MANTENIMIENTO_VC;
                        else if (i == 1) arrManteContinuoNominalVC[i] = entidad.MANTENIMIENTO_VC;
                        else arrManteContinuoNominalVC[i] = arrManteContinuoNominalVC[i - 1] * (1 + ipc);
                    }
                }
                

                //Mantenimiento continuo Neto VC
                for (var i = 0; i < 15; i++)
                {
                    arrManteContinuoNetoVC[i] = arrManteContinuoNominalVC[i] / Convert.ToDecimal(Math.Pow(Convert.ToDouble(1 + tasa_descuento), -1 + (i + 1)));
                }

                //Mantenimiento continuo Acumulado VC
                for (var i = 0; i < 15; i++)
                {
                    if (i == 0) arrManteContinuoAcumuladoVC[i] = arrManteContinuoNetoVC[i];
                    else arrManteContinuoAcumuladoVC[i] = arrManteContinuoNetoVC[i] + arrManteContinuoAcumuladoVC[i - 1];
                }

                //Depreciacion vehiculo VC
                if (entidad.P2 == "1")
                {
                    for (var i = 0; i < 15; i++)
                    {
                        if (i == 0) depreciacionVC[i] = entidad.COSTO_VEHICULO_VC;
                        else if (i == 1) depreciacionVC[i] = depreciacionVC[i - 1] * dep02;
                        else if (i == 2) depreciacionVC[i] = depreciacionVC[i - 1] * dep03;
                        else if (i == 3) depreciacionVC[i] = depreciacionVC[i - 1] * dep04;
                        else if (i == 4) depreciacionVC[i] = depreciacionVC[i - 1] * dep05;
                        else if (i == 5) depreciacionVC[i] = depreciacionVC[i - 1] * dep06;
                        else if (i == 6) depreciacionVC[i] = depreciacionVC[i - 1] * dep07;
                        else if (i == 7) depreciacionVC[i] = depreciacionVC[i - 1] * dep08;
                        else if (i == 8) depreciacionVC[i] = depreciacionVC[i - 1] * dep09;
                        else if (i == 9) depreciacionVC[i] = depreciacionVC[i - 1] * dep10;
                        else if (i == 10) depreciacionVC[i] = depreciacionVC[i - 1] * dep11;
                        else if (i == 11) depreciacionVC[i] = depreciacionVC[i - 1] * dep12;
                        else if (i == 12) depreciacionVC[i] = depreciacionVC[i - 1] * dep13;
                        else if (i == 13) depreciacionVC[i] = depreciacionVC[i - 1] * dep14;
                        else if (i == 14) depreciacionVC[i] = depreciacionVC[i - 1] * dep15;
                    }
                }                

                //Reventa Nominal VC
                arrReventaNominalVC[14] = depreciacionVC[14];

                //Reventa Neto VC
                arrReventaNetoVC[14] = arrReventaNominalVC[14] / Convert.ToDecimal(Math.Pow(Convert.ToDouble(1 + tasa_descuento), -1 + 15));

                //Reventa Acumulado VC
                arrReventaAcumuladoVC[14] = arrReventaNetoVC[14];

                //Mantenimiento extraordinario Nominal VC
                if (entidad.P2 == "1" || entidad.P1 == "1")
                {
                    for (var i = 7; i < 15; i++)
                    {
                        if (i == 7) arrManteExtraoNominalVC[i] = mante_extraordinario;
                        else arrManteExtraoNominalVC[i] = arrManteExtraoNominalVC[i - 1] * (1 + ipc);
                    }
                }                    

                //Mantenimiento extraordinario Neto VC
                for (var i = 7; i < 15; i++)
                {
                    arrManteExtraoNetoVC[i] = arrManteExtraoNominalVC[i] / Convert.ToDecimal(Math.Pow(Convert.ToDouble(1 + tasa_descuento), -1 + (i + 1)));
                }

                //Mantenimiento extraordinario Acumulado VC
                for (var i = 7; i < 15; i++)
                {
                    if (i == 7) arrManteExtraoAcumuladoVC[i] = arrManteExtraoNetoVC[i];
                    else arrManteExtraoAcumuladoVC[i] = arrManteExtraoNetoVC[i] + arrManteExtraoAcumuladoVC[i - 1];
                }

                //Servicio publico Nominal VC
                decimal costo_general_vc = 0;
                foreach (ServicioPublicoBE sp in entidad.LISTA_SERVICIO_PUBLICO)
                {
                    costo_general_vc += sp.COSTO_MOVILIDAD * sp.MESES_USO * 4;
                }

                for (var i = 0; i < 15; i++)
                {
                    if (i == 0) arrServicioPublicoNominalVC[i] = costo_general_vc;
                    else arrServicioPublicoNominalVC[i] = arrServicioPublicoNominalVC[i - 1] * (1 + ipc);
                }

                //Servicio publico Neto VC
                for (var i = 0; i < 15; i++)
                {
                    arrServicioPublicoNetoVC[i] = arrServicioPublicoNominalVC[i] / Convert.ToDecimal(Math.Pow(Convert.ToDouble(1 + tasa_descuento), -1 + (i + 1)));
                }

                //Servicio publico Acumulado VC
                for (var i = 0; i < 15; i++)
                {
                    if (i == 0) arrServicioPublicoAcumuladoVC[i] = arrServicioPublicoNetoVC[i];
                    else arrServicioPublicoAcumuladoVC[i] = arrServicioPublicoNetoVC[i] + arrServicioPublicoAcumuladoVC[i - 1];
                }

                for (int i = 0; i < 15; i++)
                {
                    EscenarioConvencionalBE ec = new EscenarioConvencionalBE();
                    ec.CUOTA_INICIAL = arrInversionInicialAcumuladoVC[i];
                    ec.INCENTIVO_ECONOMICO = 0;
                    ec.RECAMBIO_BATERIA = 0;
                    ec.SEGURO = arrSeguroAcumuladoVC[i];
                    ec.ENERGIA = arrEnergiaAcumuladoVC[i];
                    ec.MANTENIMIENTO_CONTINUO = arrManteContinuoAcumuladoVC[i];
                    ec.CARGA_FINANCIERA = arrCargaFinancieraAcumuladoVC[i];
                    ec.CARGA_INSTALACION = 0;
                    ec.REVENTA_VEHICULO = arrReventaAcumuladoVC[i] * -1;
                    ec.OTROS_TRANSPORTES = arrServicioPublicoAcumuladoVC[i];
                    ec.MANTENIMIENTO_EXTRAORDINARIO = arrManteExtraoAcumuladoVC[i];
                    ec.TOTAL = arrInversionInicialAcumuladoVC[i] + arrSeguroAcumuladoVC[i] + arrEnergiaAcumuladoVC[i] + arrManteContinuoAcumuladoVC[i] + arrCargaFinancieraAcumuladoVC[i] - arrReventaAcumuladoVC[i] + arrServicioPublicoAcumuladoVC[i] + arrManteExtraoAcumuladoVC[i];
                    lista.Add(ec);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }

            return lista;
        }

        public List<EscenarioElectromovilidadBE> CalcularVE(VehiculoElectricoBE entidad)
        {
            List<EscenarioElectromovilidadBE> lista = new List<EscenarioElectromovilidadBE>();
            try
            {
                decimal ipc = Convert.ToDecimal(AppSettings.Get<string>("dato.ipc"));
                decimal reduccion_eficiencia_motor = Convert.ToDecimal(AppSettings.Get<string>("dato.reduccioneficienciamotor"));
                decimal porc_20kmve = Convert.ToDecimal(AppSettings.Get<string>("dato.porcentaje20kmve"));
                decimal porc_100kmve = Convert.ToDecimal(AppSettings.Get<string>("dato.porcentaje100kmve"));
                decimal tasa_descuento = Convert.ToDecimal(AppSettings.Get<string>("dato.tasadescuento"));
                decimal recambio_bateria = Convert.ToDecimal(AppSettings.Get<string>("dato.recambiobateria"));

                //Porcentaje depreciacion
                decimal dep02 = Convert.ToDecimal(AppSettings.Get<string>("dato.depreciacion02"));
                decimal dep03 = Convert.ToDecimal(AppSettings.Get<string>("dato.depreciacion03"));
                decimal dep04 = Convert.ToDecimal(AppSettings.Get<string>("dato.depreciacion04"));
                decimal dep05 = Convert.ToDecimal(AppSettings.Get<string>("dato.depreciacion05"));
                decimal dep06 = Convert.ToDecimal(AppSettings.Get<string>("dato.depreciacion06"));
                decimal dep07 = Convert.ToDecimal(AppSettings.Get<string>("dato.depreciacion07"));
                decimal dep08 = Convert.ToDecimal(AppSettings.Get<string>("dato.depreciacion08"));
                decimal dep09 = Convert.ToDecimal(AppSettings.Get<string>("dato.depreciacion09"));
                decimal dep10 = Convert.ToDecimal(AppSettings.Get<string>("dato.depreciacion10"));
                decimal dep11 = Convert.ToDecimal(AppSettings.Get<string>("dato.depreciacion11"));
                decimal dep12 = Convert.ToDecimal(AppSettings.Get<string>("dato.depreciacion12"));
                decimal dep13 = Convert.ToDecimal(AppSettings.Get<string>("dato.depreciacion13"));
                decimal dep14 = Convert.ToDecimal(AppSettings.Get<string>("dato.depreciacion14"));
                decimal dep15 = Convert.ToDecimal(AppSettings.Get<string>("dato.depreciacion15"));

                decimal[] arrCargaFinancieraNominalVE = new decimal[15];
                decimal[] arrInversionInicialNominalVE = new decimal[15];
                decimal[] arrSeguroNominalVE = new decimal[15];
                decimal[] arrEnergiaNominalVE = new decimal[15];
                decimal[] arrManteContinuoNominalVE = new decimal[15];
                decimal[] depreciacionVE = new decimal[15];
                decimal[] arrReventaNominalVE = new decimal[15];
                decimal[] arrIncentivoNominalVE = new decimal[15];
                decimal[] arrRecambioNominalVE = new decimal[15];

                decimal[] arrCargaFinancieraNetoVE = new decimal[15];
                decimal[] arrInversionInicialNetoVE = new decimal[15];
                decimal[] arrSeguroNetoVE = new decimal[15];
                decimal[] arrEnergiaNetoVE = new decimal[15];
                decimal[] arrManteContinuoNetoVE = new decimal[15];
                decimal[] arrReventaNetoVE = new decimal[15];
                decimal[] arrIncentivoNetoVE = new decimal[15];
                decimal[] arrRecambioNetoVE = new decimal[15];

                decimal[] arrCargaFinancieraAcumuladoVE = new decimal[15];
                decimal[] arrInversionInicialAcumuladoVE = new decimal[15];
                decimal[] arrSeguroAcumuladoVE = new decimal[15];
                decimal[] arrEnergiaAcumuladoVE = new decimal[15];
                decimal[] arrManteContinuoAcumuladoVE = new decimal[15];
                decimal[] arrReventaAcumuladoVE = new decimal[15];
                decimal[] arrIncentivoAcumuladoVE = new decimal[15];
                decimal[] arrRecambioAcumuladoVE = new decimal[15];
                decimal[] arrCargaInstalacionAcumuladoVE = new decimal[15];

                //Incentivo Nominal VE
                decimal incentivo_unico = 0;
                if (entidad.P_INCENTIVO == "1")
                {
                    if (entidad.TIPO_INCENTIVO_VE == 1)
                    {
                        for (var i = 0; i < entidad.HORIZONTE; i++)
                        {
                            arrIncentivoNominalVE[i] = entidad.CUOTA_INCENTIVO_ANUAL;
                        }

                        //Incentivo Neto VE
                        for (var i = 0; i < 15; i++)
                        {
                            arrIncentivoNetoVE[i] = arrIncentivoNominalVE[i] / Convert.ToDecimal(Math.Pow(Convert.ToDouble(1 + tasa_descuento), -1 + (i + 1)));
                        }

                        //Incentivo Acumulado VE
                        for (var i = 0; i < 15; i++)
                        {
                            if (i == 0) arrIncentivoAcumuladoVE[i] = arrIncentivoNetoVE[i];
                            else arrIncentivoAcumuladoVE[i] = arrIncentivoNetoVE[i] + arrIncentivoAcumuladoVE[i - 1];
                        }
                    }
                    else if (entidad.TIPO_INCENTIVO_VE == 1)
                    {
                        if (entidad.FORMA_INCENTIVO_VE == 1)
                        {
                            incentivo_unico = entidad.COSTO_VEHICULO_VE * entidad.PORCENTAJE_INCENTIVO;
                        }
                        else if (entidad.FORMA_INCENTIVO_VE == 2)
                        {
                            incentivo_unico = entidad.INCENTIVO_UNICO;
                        }
                    }
                }

                //Recambio bateria Nominal VE
                arrRecambioNominalVE[7] = entidad.COSTO_VEHICULO_VE * recambio_bateria;

                //Recambio bateria Neto VE
                arrRecambioNetoVE[7] = arrRecambioNominalVE[7] / Convert.ToDecimal(Math.Pow(Convert.ToDouble(1 + tasa_descuento), -1 + 8));

                //Recambio bateria Acumulado VE
                for (var i = 7; i < 15; i++)
                {
                    if (i == 7) arrRecambioAcumuladoVE[i] = arrRecambioNetoVE[i];
                    else arrRecambioAcumuladoVE[i] = arrRecambioNetoVE[i] + arrRecambioAcumuladoVE[i - 1];
                }

                //Mantenimiento continuo Nominal VE
                decimal mantenim_20kmve = entidad.COSTO_VEHICULO_VE * porc_20kmve;
                decimal mantenim_100kmve = entidad.COSTO_VEHICULO_VE * porc_100kmve;
                for (int i = 0; i < 15; i++)
                {
                    if (i == 0) arrManteContinuoNominalVE[i] = mantenim_20kmve;
                    else if (i == 1) arrManteContinuoNominalVE[i] = mantenim_100kmve;
                    else arrManteContinuoNominalVE[i] = arrManteContinuoNominalVE[i - 1] * (1 + ipc);
                }

                //Mantenimiento continuo Neto VE
                for (var i = 0; i < 15; i++)
                {
                    arrManteContinuoNetoVE[i] = arrManteContinuoNominalVE[i] / Convert.ToDecimal(Math.Pow(Convert.ToDouble(1 + tasa_descuento), -1 + (i + 1)));
                }

                //Mantenimiento continuo Acumulado VE
                for (var i = 0; i < 15; i++)
                {
                    if (i == 0) arrManteContinuoAcumuladoVE[i] = arrManteContinuoNetoVE[i];
                    else arrManteContinuoAcumuladoVE[i] = arrManteContinuoNetoVE[i] + arrManteContinuoAcumuladoVE[i - 1];
                }

                //Depreciacion vehiculo VE
                for (var i = 0; i < 15; i++)
                {
                    if (i == 0) depreciacionVE[i] = entidad.COSTO_VEHICULO_VE;
                    else if (i == 1) depreciacionVE[i] = depreciacionVE[i - 1] * dep02;
                    else if (i == 2) depreciacionVE[i] = depreciacionVE[i - 1] * dep03;
                    else if (i == 3) depreciacionVE[i] = depreciacionVE[i - 1] * dep04;
                    else if (i == 4) depreciacionVE[i] = depreciacionVE[i - 1] * dep05;
                    else if (i == 5) depreciacionVE[i] = depreciacionVE[i - 1] * dep06;
                    else if (i == 6) depreciacionVE[i] = depreciacionVE[i - 1] * dep07;
                    else if (i == 7) depreciacionVE[i] = depreciacionVE[i - 1] * dep08;
                    else if (i == 8) depreciacionVE[i] = depreciacionVE[i - 1] * dep09;
                    else if (i == 9) depreciacionVE[i] = depreciacionVE[i - 1] * dep10;
                    else if (i == 10) depreciacionVE[i] = depreciacionVE[i - 1] * dep11;
                    else if (i == 11) depreciacionVE[i] = depreciacionVE[i - 1] * dep12;
                    else if (i == 12) depreciacionVE[i] = depreciacionVE[i - 1] * dep13;
                    else if (i == 13) depreciacionVE[i] = depreciacionVE[i - 1] * dep14;
                    else if (i == 14) depreciacionVE[i] = depreciacionVE[i - 1] * dep15;
                }

                //Reventa Nominal VE
                arrReventaNominalVE[14] = depreciacionVE[14];

                //Reventa Neto VE
                arrReventaNetoVE[14] = arrReventaNominalVE[14] / Convert.ToDecimal(Math.Pow(Convert.ToDouble(1 + tasa_descuento), -1 + 15));

                //Reventa Acumulado VE
                arrReventaAcumuladoVE[14] = arrReventaNetoVE[14];

                //Cuota inicial y carga financiera Nominal VE             
                decimal cuota_inicial = 0;
                entidad.COSTO_VEHICULO_VE -= incentivo_unico;
                if (entidad.TIPO_COMPRA_VE == 1)
                {
                    cuota_inicial = entidad.COSTO_VEHICULO_VE * entidad.PORC_CUOTA_INICIAL_VE;
                    decimal primera_carga = (entidad.COSTO_VEHICULO_VE - cuota_inicial) * (1 + entidad.TASA_INTERES_VE);
                    for (int i = 0; i < entidad.ANIO_CREDITO_VE; i++)
                    {
                        if (i == 0) arrCargaFinancieraNominalVE[i] = primera_carga / entidad.ANIO_CREDITO_VE;
                        else arrCargaFinancieraNominalVE[i] = (primera_carga / entidad.ANIO_CREDITO_VE) * (1 + ipc);
                    }
                }
                else if (entidad.TIPO_COMPRA_VE == 2)
                {
                    cuota_inicial = entidad.COSTO_VEHICULO_VE;
                }
                arrInversionInicialNominalVE[0] = cuota_inicial;

                //Cuota inicial Neto VC  
                arrInversionInicialNetoVE[0] = arrInversionInicialNominalVE[0] / Convert.ToDecimal(Math.Pow(Convert.ToDouble(1 + tasa_descuento), -1 + 1));

                //Cuota inicial Acumulado VC 
                for (var i = 0; i < 15; i++)
                {
                    if (i == 0) arrInversionInicialAcumuladoVE[i] = arrInversionInicialNetoVE[i];
                    else arrInversionInicialAcumuladoVE[i] = arrInversionInicialNetoVE[i] + arrInversionInicialAcumuladoVE[i - 1];
                }

                //carga financiera Neto VE
                for (var i = 0; i < entidad.ANIO_CREDITO_VE; i++)
                {
                    arrCargaFinancieraNetoVE[i] = arrCargaFinancieraNominalVE[i] / Convert.ToDecimal(Math.Pow(Convert.ToDouble(1 + tasa_descuento), -1 + (i + 1)));
                }

                //carga financiera Acumulado VC
                for (var i = 0; i < 15; i++)
                {
                    if (i == 0) arrCargaFinancieraAcumuladoVE[i] = arrCargaFinancieraNetoVE[i];
                    else arrCargaFinancieraAcumuladoVE[i] = arrCargaFinancieraNetoVE[i] + arrCargaFinancieraAcumuladoVE[i - 1];
                }

                //Seguro Nominal VC
                if (entidad.P_SEGURO_VE == "1")
                {
                    for (int i = 0; i < 15; i++)
                    {
                        if (i == 0) arrSeguroNominalVE[i] = entidad.SEGURO_VE;
                        else arrSeguroNominalVE[i] = arrSeguroNominalVE[i - 1] * (1 + ipc);
                    }
                }

                //Seguro Neto VC
                for (var i = 0; i < 15; i++)
                {
                    arrSeguroNetoVE[i] = arrSeguroNominalVE[i] / Convert.ToDecimal(Math.Pow(Convert.ToDouble(1 + tasa_descuento), -1 + (i + 1)));
                }

                //Seguro Acumulado VC
                for (var i = 0; i < 15; i++)
                {
                    if (i == 0) arrSeguroAcumuladoVE[i] = arrSeguroNetoVE[i];
                    else arrSeguroAcumuladoVE[i] = arrSeguroNetoVE[i] + arrSeguroAcumuladoVE[i - 1];
                }

                //Energia (Electricidad y combustible) Nominal VC
                decimal km_anual_ve = (entidad.KILOMETRO_SEMANAL_VE * 52) * (entidad.MESES_USO_VE / 12);
                decimal energia_ve = (km_anual_ve / entidad.RENDIMIENTO_VE) * entidad.TARIFA_VE;
                for (var i = 0; i < 15; i++)
                {
                    if (i == 0) arrEnergiaNominalVE[i] = energia_ve;
                    else arrEnergiaNominalVE[i] = arrEnergiaNominalVE[i - 1] * (1 + ipc) * (1 + entidad.PORCENTAJE_ANUAL_VE);
                }

                //Energia (Electricidad y combustible) Neto VC
                for (var i = 0; i < 15; i++)
                {
                    arrEnergiaNetoVE[i] = arrEnergiaNominalVE[i] / Convert.ToDecimal(Math.Pow(Convert.ToDouble(1 + tasa_descuento), -1 + (i + 1)));
                }

                //Energia (Electricidad y combustible) Acumulado VC
                for (var i = 0; i < 15; i++)
                {
                    if (i == 0) arrEnergiaAcumuladoVE[i] = arrEnergiaNetoVE[i];
                    else arrEnergiaAcumuladoVE[i] = arrEnergiaNetoVE[i] + arrEnergiaAcumuladoVE[i - 1];
                }

                //Equipo carga e instalacion Nominal VE
                decimal carga_instalacion = entidad.PRECIO_CARGADOR + entidad.COSTO_INSTALACION;

                //Equipo carga e instalacion Acumulado VE
                for (var i = 0; i < 15; i++)
                {
                    if (i == 0) arrCargaInstalacionAcumuladoVE[i] = carga_instalacion;
                    else arrCargaInstalacionAcumuladoVE[i] = 0 + arrCargaInstalacionAcumuladoVE[i - 1];
                }

                for (int i = 0; i < 15; i++)
                {
                    EscenarioElectromovilidadBE ec = new EscenarioElectromovilidadBE();
                    ec.CUOTA_INICIAL = arrInversionInicialAcumuladoVE[i];
                    ec.INCENTIVO_ECONOMICO = arrIncentivoAcumuladoVE[i] * -1;
                    ec.RECAMBIO_BATERIA = arrRecambioAcumuladoVE[i];
                    ec.SEGURO = arrSeguroAcumuladoVE[i];
                    ec.ENERGIA = arrEnergiaAcumuladoVE[i];
                    ec.MANTENIMIENTO_CONTINUO = arrManteContinuoAcumuladoVE[i];
                    ec.CARGA_FINANCIERA = arrCargaFinancieraAcumuladoVE[i];
                    ec.CARGA_INSTALACION = arrCargaInstalacionAcumuladoVE[i];
                    ec.REVENTA_VEHICULO = arrReventaAcumuladoVE[i] * -1;
                    ec.OTROS_TRANSPORTES = 0;
                    ec.MANTENIMIENTO_EXTRAORDINARIO = 0;
                    ec.TOTAL = arrInversionInicialAcumuladoVE[i] + arrSeguroAcumuladoVE[i] + arrEnergiaAcumuladoVE[i] + arrManteContinuoAcumuladoVE[i] + arrCargaFinancieraAcumuladoVE[i] + arrCargaInstalacionAcumuladoVE[i] + arrRecambioAcumuladoVE[i] - arrReventaAcumuladoVE[i] - arrIncentivoAcumuladoVE[i];
                    lista.Add(ec);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }

            return lista;
        }
    }
}
