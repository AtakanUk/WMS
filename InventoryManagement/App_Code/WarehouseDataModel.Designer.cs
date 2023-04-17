﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
namespace WarehouseDBModel
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class WarehouseDBEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new WarehouseDBEntities object using the connection string found in the 'WarehouseDBEntities' section of the application configuration file.
        /// </summary>
        public WarehouseDBEntities() : base("name=WarehouseDBEntities", "WarehouseDBEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new WarehouseDBEntities object.
        /// </summary>
        public WarehouseDBEntities(string connectionString) : base(connectionString, "WarehouseDBEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new WarehouseDBEntities object.
        /// </summary>
        public WarehouseDBEntities(EntityConnection connection) : base(connection, "WarehouseDBEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<DriverInfo> DriverInfoes
        {
            get
            {
                if ((_DriverInfoes == null))
                {
                    _DriverInfoes = base.CreateObjectSet<DriverInfo>("DriverInfoes");
                }
                return _DriverInfoes;
            }
        }
        private ObjectSet<DriverInfo> _DriverInfoes;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<ProductInfo> ProductInfoes
        {
            get
            {
                if ((_ProductInfoes == null))
                {
                    _ProductInfoes = base.CreateObjectSet<ProductInfo>("ProductInfoes");
                }
                return _ProductInfoes;
            }
        }
        private ObjectSet<ProductInfo> _ProductInfoes;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the DriverInfoes EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToDriverInfoes(DriverInfo driverInfo)
        {
            base.AddObject("DriverInfoes", driverInfo);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the ProductInfoes EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToProductInfoes(ProductInfo productInfo)
        {
            base.AddObject("ProductInfoes", productInfo);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="WarehouseDBModel", Name="DriverInfo")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class DriverInfo : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new DriverInfo object.
        /// </summary>
        /// <param name="driverID">Initial value of the DriverID property.</param>
        public static DriverInfo CreateDriverInfo(global::System.Int32 driverID)
        {
            DriverInfo driverInfo = new DriverInfo();
            driverInfo.DriverID = driverID;
            return driverInfo;
        }

        #endregion

        #region Simple Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 DriverID
        {
            get
            {
                return _DriverID;
            }
            set
            {
                if (_DriverID != value)
                {
                    OnDriverIDChanging(value);
                    ReportPropertyChanging("DriverID");
                    _DriverID = StructuralObject.SetValidValue(value, "DriverID");
                    ReportPropertyChanged("DriverID");
                    OnDriverIDChanged();
                }
            }
        }
        private global::System.Int32 _DriverID;
        partial void OnDriverIDChanging(global::System.Int32 value);
        partial void OnDriverIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String DriverName
        {
            get
            {
                return _DriverName;
            }
            set
            {
                OnDriverNameChanging(value);
                ReportPropertyChanging("DriverName");
                _DriverName = StructuralObject.SetValidValue(value, true, "DriverName");
                ReportPropertyChanged("DriverName");
                OnDriverNameChanged();
            }
        }
        private global::System.String _DriverName;
        partial void OnDriverNameChanging(global::System.String value);
        partial void OnDriverNameChanged();

        #endregion

    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="WarehouseDBModel", Name="ProductInfo")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class ProductInfo : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new ProductInfo object.
        /// </summary>
        /// <param name="productID">Initial value of the ProductID property.</param>
        public static ProductInfo CreateProductInfo(global::System.Int32 productID)
        {
            ProductInfo productInfo = new ProductInfo();
            productInfo.ProductID = productID;
            return productInfo;
        }

        #endregion

        #region Simple Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ProductID
        {
            get
            {
                return _ProductID;
            }
            set
            {
                if (_ProductID != value)
                {
                    OnProductIDChanging(value);
                    ReportPropertyChanging("ProductID");
                    _ProductID = StructuralObject.SetValidValue(value, "ProductID");
                    ReportPropertyChanged("ProductID");
                    OnProductIDChanged();
                }
            }
        }
        private global::System.Int32 _ProductID;
        partial void OnProductIDChanging(global::System.Int32 value);
        partial void OnProductIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String ProductName
        {
            get
            {
                return _ProductName;
            }
            set
            {
                OnProductNameChanging(value);
                ReportPropertyChanging("ProductName");
                _ProductName = StructuralObject.SetValidValue(value, true, "ProductName");
                ReportPropertyChanged("ProductName");
                OnProductNameChanged();
            }
        }
        private global::System.String _ProductName;
        partial void OnProductNameChanging(global::System.String value);
        partial void OnProductNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> ProductCount
        {
            get
            {
                return _ProductCount;
            }
            set
            {
                OnProductCountChanging(value);
                ReportPropertyChanging("ProductCount");
                _ProductCount = StructuralObject.SetValidValue(value, "ProductCount");
                ReportPropertyChanged("ProductCount");
                OnProductCountChanged();
            }
        }
        private Nullable<global::System.Int32> _ProductCount;
        partial void OnProductCountChanging(Nullable<global::System.Int32> value);
        partial void OnProductCountChanged();

        #endregion

    }

    #endregion

}
