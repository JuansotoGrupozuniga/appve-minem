--------------------------------------------------------
-- Archivo creado  - mi�rcoles-marzo-03-2021   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Package PKG_ELECTROMOV_CALCULO
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "ELECTROMOVILIDAD"."PKG_ELECTROMOV_CALCULO" AS 

  PROCEDURE USP_SEL_LIST_TIPO_COMBUS(
    PO_REF OUT SYS_REFCURSOR
  );
  
  PROCEDURE USP_SEL_LIST_TIPO_TRANSP(
    PO_REF OUT SYS_REFCURSOR
  );
  
  PROCEDURE USP_SEL_LIST_TIPO_VEH_CONV(
    PO_REF OUT SYS_REFCURSOR
  );
  
  PROCEDURE USP_SEL_LIST_TIPO_VEH_ELEC(
    PO_REF OUT SYS_REFCURSOR
  );
  
  PROCEDURE USP_SEL_GET_FORMULA(
    PI_ID_CASO NUMBER,
    PO_REF OUT SYS_REFCURSOR
  );
  
  PROCEDURE USP_SEL_FACTOR_PARAMETRO(
    PI_ID_FACTOR NUMBER,
    PO_REF OUT SYS_REFCURSOR
  );
  
  PROCEDURE USP_SEL_FACTOR_VALOR(
    PI_ID_FACTOR NUMBER,
    PI_SQL_WHERE VARCHAR2,
    PO_REF OUT SYS_REFCURSOR
  );
  
  PROCEDURE USP_MAN_GUARDA_INSTITUCION(
    PI_ID_INSTITUCION NUMBER,
    PI_RUC VARCHAR2,
    PI_RAZON_SOCIAL VARCHAR2,
    PI_CORREO VARCHAR2,
    PI_TELEFONO VARCHAR2,
    PI_DIRECCION VARCHAR2,
    PI_UPD_USUARIO NUMBER,
    PI_ID_GET OUT NUMBER,
    PO_ROWAFFECTED OUT NUMBER
  );
  
  PROCEDURE USP_SEL_LISTA_FACTOR_1P(
    PI_ID_FACTOR NUMBER,
    PI_ID_P1 NUMBER,
    PI_V_P1 NUMBER,
    PO_REF OUT SYS_REFCURSOR
  );
  
  PROCEDURE USP_SEL_LISTA_FACTOR_2P(
    PI_ID_FACTOR NUMBER,
    PI_ID_P1 NUMBER,
    PI_ID_P2 NUMBER,
    PI_V_P1 NUMBER,
    PI_V_P2 NUMBER,
    PO_REF OUT SYS_REFCURSOR
  );
  
  PROCEDURE USP_SEL_LISTA_FACTOR_3P(
    PI_ID_FACTOR NUMBER,
    PI_ID_P1 NUMBER,
    PI_ID_P2 NUMBER,
    PI_ID_P3 NUMBER,
    PI_V_P1 NUMBER,
    PI_V_P2 NUMBER,
    PI_V_P3 NUMBER,
    PO_REF OUT SYS_REFCURSOR
  );
  
  PROCEDURE USP_SEL_LIST_MODELO_VEH(
    PO_REF OUT SYS_REFCURSOR
  );
  
  PROCEDURE USP_SEL_LIST_TIPO_CARGADOR(
    PO_REF OUT SYS_REFCURSOR
  );
  
  PROCEDURE USP_SEL_LIST_CARGADOR_POTENC(
    PO_REF OUT SYS_REFCURSOR
  );
  
  PROCEDURE USP_SEL_LIST_DEPARTAMENTO(
    PO_REF OUT SYS_REFCURSOR
  );
  
  PROCEDURE USP_PRC_GUARDAR_INSTITUCION(
    PI_ID_INSTITUCION NUMBER,
    PI_RUC VARCHAR2,
    PI_RAZON_SOCIAL VARCHAR2,
    PI_CORREO VARCHAR2,
    PI_TELEFONO VARCHAR2,
    PI_DIRECCION VARCHAR2,    
    PI_UPD_USUARIO NUMBER,
    PI_ID_GET IN OUT NUMBER,
    PO_ROWAFFECTED OUT NUMBER
  );
  
  PROCEDURE USP_PRC_GUARDAR_ESTACION(
    PI_ID_ESTACION NUMBER,
    PI_ID_USUARIO NUMBER,
    PI_DESCRIPCION VARCHAR2,
    PI_MODELO VARCHAR2,
    PI_MARCA VARCHAR2,
    PI_POTENCIA NUMBER,
    PI_MODO_CARGA VARCHAR2,
    PI_TIPO_CARGADOR VARCHAR2,
    PI_TIPO_CONECTOR VARCHAR2,
    PI_CANTIDAD_CONECTOR NUMBER,
    PI_HORA_DESDE VARCHAR2,
    PI_HORA_HASTA VARCHAR2,
    PI_TARIFA_SERVICIO NUMBER,
    PI_ID_ESTADO NUMBER,
    PI_UPD_USUARIO NUMBER,
    PI_ID_GET IN OUT NUMBER,
    PO_ROWAFFECTED OUT NUMBER
  );
  
  PROCEDURE USP_PRC_MAN_DOCUMENTO_DATA(
    PI_ID_DOCUMENTO NUMBER,
    PI_ID_ESTACION VARCHAR2,
    PI_ARCHIVO_BASE VARCHAR2,   
    PI_UPD_USUARIO NUMBER,
    PO_ROWAFFECTED OUT NUMBER
  );
  
  PROCEDURE USP_PRC_MAN_DOCUMENTO_IMG(
    PI_ID_DOCUMENTO NUMBER,
    PI_ID_ESTACION VARCHAR2,
    PI_ARCHIVO_BASE VARCHAR2,
    PI_FLAG_ESTADO VARCHAR2,
    PI_UPD_USUARIO NUMBER,
    PO_ROWAFFECTED OUT NUMBER
  );
  
  PROCEDURE USP_DEL_DOCUMENTO_IMG(
    PI_ID_ESTACION NUMBER
  );
  
  PROCEDURE USP_SEL_ESTACION_USUARIO(
    PI_ID_USUARIO NUMBER,
    PO_REF OUT SYS_REFCURSOR
  );
  
  PROCEDURE USP_SEL_ALL_DOCUMENTO(
    PO_REF OUT SYS_REFCURSOR,
    PI_ID_ESTACION NUMBER
  );
  
  PROCEDURE USP_SEL_ALL_IMAGEN(
    PO_REF OUT SYS_REFCURSOR,
    PI_ID_ESTACION NUMBER
  );
  
  PROCEDURE USP_SEL_USUARIO_INSTITUCION(
    PI_ID_USUARIO NUMBER,
    PO_REF OUT SYS_REFCURSOR    
  );
  
  PROCEDURE USP_SEL_ESTACION(
    PI_ID_ESTACION NUMBER,
    PO_REF OUT SYS_REFCURSOR
  );
  
  PROCEDURE USP_DEL_ESTACION(
    PI_ID_ESTACION NUMBER,
    PO_ROWAFFECTED OUT NUMBER
  );
  
  PROCEDURE USP_INS_GENERAR_DETALLE_RESULT(
    PI_ID_USUARIO NUMBER,
    PI_UPD_USUARIO NUMBER,
    PI_ID_GET OUT NUMBER,
    PO_ROWAFFECTED OUT NUMBER
  );
  
  PROCEDURE USP_INS_GUARDAR_LEYENDA(
    PI_ID_RESULTADO NUMBER,
    PI_ID_USUARIO NUMBER,
    PI_ID_DETALLE NUMBER,
    PI_NOMBRE_TRANSPORTE VARCHAR2,
    PI_UPD_USUARIO NUMBER,
    PO_ROWAFFECTED OUT NUMBER
  );
  
  PROCEDURE USP_INS_GUARDAR_COSTO_VC(
    PI_ID_RESULTADO NUMBER,
    PI_ID_USUARIO NUMBER,
    PI_ID_DETALLE NUMBER,
    PI_CUOTA_INICIAL NUMBER,
    PI_INCENTIVO_ECONOMICO NUMBER,
    PI_RECAMBIO_BATERIA NUMBER,
    PI_SEGURO NUMBER,
    PI_ENERGIA NUMBER,
    PI_MANTENIMIENTO_CONTINUO NUMBER,
    PI_CARGA_FINANCIERA NUMBER,
    PI_CARGA_INSTALACION NUMBER,
    PI_REVENTA_VEHICULO NUMBER,
    PI_OTROS_TRANSPORTES NUMBER,
    PI_MANTENIMIENTO_EXTRAORDINA NUMBER,
    PI_TOTAL NUMBER,
    PI_UPD_USUARIO NUMBER,
    PO_ROWAFFECTED OUT NUMBER
  );
  
  PROCEDURE USP_INS_GUARDAR_COSTO_VE(
    PI_ID_RESULTADO NUMBER,
    PI_ID_USUARIO NUMBER,
    PI_ID_DETALLE NUMBER,
    PI_CUOTA_INICIAL NUMBER,
    PI_INCENTIVO_ECONOMICO NUMBER,
    PI_RECAMBIO_BATERIA NUMBER,
    PI_SEGURO NUMBER,
    PI_ENERGIA NUMBER,
    PI_MANTENIMIENTO_CONTINUO NUMBER,
    PI_CARGA_FINANCIERA NUMBER,
    PI_CARGA_INSTALACION NUMBER,
    PI_REVENTA_VEHICULO NUMBER,
    PI_OTROS_TRANSPORTES NUMBER,
    PI_MANTENIMIENTO_EXTRAORDINA NUMBER,
    PI_TOTAL NUMBER,
    PI_UPD_USUARIO NUMBER,
    PO_ROWAFFECTED OUT NUMBER
  );
  
  PROCEDURE USP_INS_GUARDAR_CONSUMO_VC(
    PI_ID_RESULTADO NUMBER,
    PI_ID_USUARIO NUMBER,
    PI_ID_DETALLE NUMBER,
    PI_VEHICULO_PROPIO_VC NUMBER,
    PI_SERVICIO_PUBLICO_1 NUMBER,
    PI_SERVICIO_PUBLICO_2 NUMBER,
    PI_SERVICIO_PUBLICO_3 NUMBER,
    PI_SERVICIO_PUBLICO_4 NUMBER,
    PI_TOTAL_PUBLICO NUMBER,
    PI_UPD_USUARIO NUMBER,
    PO_ROWAFFECTED OUT NUMBER
  );
  
  PROCEDURE USP_INS_GUARDAR_CONSUMO_VE(
    PI_ID_RESULTADO NUMBER,
    PI_ID_USUARIO NUMBER,
    PI_ID_DETALLE NUMBER,
    PI_VEHICULO_PROPIO_VE NUMBER,
    PI_SERVICIO_PUBLICO_1 NUMBER,
    PI_SERVICIO_PUBLICO_2 NUMBER,
    PI_SERVICIO_PUBLICO_3 NUMBER,
    PI_SERVICIO_PUBLICO_4 NUMBER,
    PI_TOTAL_PUBLICO NUMBER,
    PI_UPD_USUARIO NUMBER,
    PO_ROWAFFECTED OUT NUMBER
  );
  
  PROCEDURE USP_INS_GUARDAR_EMISIONES_VC(
    PI_ID_RESULTADO NUMBER,
    PI_ID_USUARIO NUMBER,
    PI_ID_DETALLE NUMBER,
    PI_FABRICACION_BATERIA_VC NUMBER,
    PI_OPERACION_VEHICULO_VC NUMBER,
    PI_FABRICACION_VEHICULO_VC NUMBER,
    PI_SERVICIO_TRANSPORTE NUMBER,
    PI_TOTAL_VC NUMBER,
    PI_UPD_USUARIO NUMBER,
    PO_ROWAFFECTED OUT NUMBER
  );
  
  PROCEDURE USP_INS_GUARDAR_EMISIONES_VE(
    PI_ID_RESULTADO NUMBER,
    PI_ID_USUARIO NUMBER,
    PI_ID_DETALLE NUMBER,
    PI_FABRICACION_BATERIA_VE NUMBER,
    PI_OPERACION_VEHICULO_VE NUMBER,
    PI_FABRICACION_VEHICULO_VE NUMBER,
    PI_SERVICIO_TRANSPORTE NUMBER,
    PI_TOTAL_VE NUMBER,
    PI_UPD_USUARIO NUMBER,
    PO_ROWAFFECTED OUT NUMBER
  );
  
  PROCEDURE USP_INS_GUARDAR_CONTAMINANTE(
    PI_ID_RESULTADO NUMBER,
    PI_ID_USUARIO NUMBER,
    PI_ID_DETALLE NUMBER,
    PI_TOTAL_NOX NUMBER,
    PI_TOTAL_CO NUMBER,
    PI_TOTAL_PM25 NUMBER,
    PI_TOTAL_BC NUMBER,
    PI_UPD_USUARIO NUMBER,
    PO_ROWAFFECTED OUT NUMBER
  );
  
  PROCEDURE USP_SEL_LISTA_RESULTADOS(
    PI_ID_USUARIO NUMBER,
    PO_REF OUT SYS_REFCURSOR
  );
  
  PROCEDURE USP_SEL_LISTA_LEYENDA(
    PI_ID_USUARIO NUMBER,
    PI_ID_RESULTADO NUMBER,
    PO_REF OUT SYS_REFCURSOR
  );
  
  PROCEDURE USP_SEL_LISTA_COSTO_VC(
    PI_ID_USUARIO NUMBER,
    PI_ID_RESULTADO NUMBER,
    PO_REF OUT SYS_REFCURSOR
  );
  
  PROCEDURE USP_SEL_LISTA_COSTO_VE(
    PI_ID_USUARIO NUMBER,
    PI_ID_RESULTADO NUMBER,
    PO_REF OUT SYS_REFCURSOR
  );
  
  PROCEDURE USP_SEL_LISTA_CONSUMO_VC(
    PI_ID_USUARIO NUMBER,
    PI_ID_RESULTADO NUMBER,
    PO_REF OUT SYS_REFCURSOR
  );
  
  PROCEDURE USP_SEL_LISTA_CONSUMO_VE(
    PI_ID_USUARIO NUMBER,
    PI_ID_RESULTADO NUMBER,
    PO_REF OUT SYS_REFCURSOR
  );
  
  PROCEDURE USP_SEL_LISTA_EMISIONES_VC(
    PI_ID_USUARIO NUMBER,
    PI_ID_RESULTADO NUMBER,
    PO_REF OUT SYS_REFCURSOR
  );
  
  PROCEDURE USP_SEL_LISTA_EMISIONES_VE(
    PI_ID_USUARIO NUMBER,
    PI_ID_RESULTADO NUMBER,
    PO_REF OUT SYS_REFCURSOR
  );
  
  PROCEDURE USP_SEL_LISTA_CONTAMINANTE(
    PI_ID_USUARIO NUMBER,
    PI_ID_RESULTADO NUMBER,
    PO_REF OUT SYS_REFCURSOR
  );
  
  PROCEDURE USP_DEL_RESULTADO(
    PI_ID_RESULTADO NUMBER,
    PI_UPD_USUARIO NUMBER,
    PO_ROWAFFECTED OUT NUMBER
  );

END PKG_ELECTROMOV_CALCULO;

/
