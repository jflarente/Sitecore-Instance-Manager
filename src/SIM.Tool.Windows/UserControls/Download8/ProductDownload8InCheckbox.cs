﻿namespace SIM.Tool.Windows.UserControls.Download8
{
  using System;
  using System.Linq;
  using SIM.Products;
  using Sitecore.Diagnostics.Base;
  using JetBrains.Annotations;
  using Sitecore.Diagnostics.InfoService.Client.Model;
  using SIM.Extensions;

  public class ProductDownload8InCheckbox : DataObjectBase
  {
    #region Fields

    private readonly bool isEnabled;
    private readonly string label;
    private readonly string name;
    private readonly string nameOverride;
    private readonly string revision;
    private readonly string version;
    private bool isChecked;

    private Uri value;

    #endregion

    #region Constructors

    public ProductDownload8InCheckbox([NotNull] IRelease release)
    {
      Assert.ArgumentNotNull(release, nameof(release));

      this.name = "Sitecore CMS";
      this.version = release.Version.MajorMinor;
      this.revision = release.Version.Revision.ToString();
      this.label = release.Label;
      var distribution = release.DefaultDistribution;
      Assert.IsNotNull(distribution, nameof(distribution));

      this.value = new Uri(distribution.Downloads.First(x => x.StartsWith("http")));
      this.isEnabled = !ProductManager.Products.Any(this.CheckProduct);
    }

    #endregion

    #region Public methods

    public override string ToString()
    {
      return $"{this.nameOverride ?? this.Name} {this.Version} rev. {this.Revision}{(string.IsNullOrEmpty(this.Label) ? string.Empty : " (" + this.Label + ")")}{(this.IsEnabled ? string.Empty : " - you already have it")}";
    }

    #endregion

    #region Private methods

    private bool CheckAnalyticsProduct(Products.Product product)
    {
      return product.Name.Equals("Sitecore Analytics")
             && product.Revision == this.revision;
    }

    private bool CheckProduct([CanBeNull] Products.Product product)
    {
      if (product == null)
      {
        return false;
      }

      if (!product.Name.EqualsIgnoreCase(this.name) && !product.OriginalName.EqualsIgnoreCase(this.name))
      {
        return false;
      }

      if (product.Version != this.version)
      {
        return false;
      }

      return product.Revision == this.revision;
    }

    #endregion

    #region Properties

    #region Public properties

    public bool IsChecked
    {
      get
      {
        return this.isChecked;
      }

      set
      {
        this.isChecked = value;
        this.NotifyPropertyChanged("IsChecked");
      }
    }

    public bool IsEnabled
    {
      get
      {
        return this.isEnabled;
      }
    }

    public string Name
    {
      get
      {
        return this.name;
      }
    }

    public Uri Value
    {
      get
      {
        return this.value;
      }

      set
      {
        this.value = value;
        this.NotifyPropertyChanged("Value");
      }
    }

    #endregion

    #region Protected properties

    protected string Label
    {
      get
      {
        return this.label;
      }
    }

    protected string Revision
    {
      get
      {
        return this.revision;
      }
    }

    protected string Version
    {
      get
      {
        return this.version;
      }
    }

    #endregion

    #endregion
  }
}