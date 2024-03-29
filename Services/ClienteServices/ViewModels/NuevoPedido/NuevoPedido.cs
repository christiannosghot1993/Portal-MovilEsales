﻿using Portal_MovilEsales.Services.ClienteServices.ViewModels.FamilaProducto;

namespace Portal_MovilEsales.Services.ClienteServices.ViewModels.NuevoPedido
{
    public class NuevoPedido
    {
        public CargaCabeceraPedido cargaCabeceraPedido { get; set; } = new ();

        public List<FamiliaProducto> listaFamiliaProductos { get; set; }

        //public List<ProductoPorFamilia> listaProductosPorFamilia { get; set; } = new();

        public ListadoProductosFavoritos listaProductosPorFamilia { get; set; } = new();

        public ListadoProductosFavoritos listadProductosFavoritos { get; set; }

        public List<ProductosNuevoPedido> listadoProductosNuevoPedido { get; set; } = new();

        public ResumenDetalleProductos resumenDetalleProductos { get; set; } = new();

        public string mensajeProceso { get; set; }

    }
}
