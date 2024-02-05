using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace MTLXImporter;

// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[Serializable()]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
[XmlRoot(Namespace = "", IsNullable = false)]
public partial class materialx
{

    private object[] itemsField;

    private decimal versionField;

    private string fileprefixField;

    /// <remarks/>
    [XmlElement("displacement", typeof(materialxDisplacement))]
    [XmlElement("normalmap", typeof(materialxNormalmap))]
    [XmlElement("standard_surface", typeof(materialxStandard_surface))]
    [XmlElement("surfacematerial", typeof(materialxSurfacematerial))]
    [XmlElement("tiledimage", typeof(materialxTiledimage))]
    public object[] Items
    {
        get
        {
            return itemsField;
        }
        set
        {
            itemsField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public decimal version
    {
        get
        {
            return versionField;
        }
        set
        {
            versionField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string fileprefix
    {
        get
        {
            return fileprefixField;
        }
        set
        {
            fileprefixField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
public partial class materialxDisplacement
{

    private materialxDisplacementInput[] inputField;

    private string nameField;

    private decimal xposField;

    private string typeField;

    private decimal yposField;

    /// <remarks/>
    [XmlElement("input")]
    public materialxDisplacementInput[] input
    {
        get
        {
            return inputField;
        }
        set
        {
            inputField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string name
    {
        get
        {
            return nameField;
        }
        set
        {
            nameField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public decimal xpos
    {
        get
        {
            return xposField;
        }
        set
        {
            xposField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string type
    {
        get
        {
            return typeField;
        }
        set
        {
            typeField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public decimal ypos
    {
        get
        {
            return yposField;
        }
        set
        {
            yposField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
public partial class materialxDisplacementInput
{

    private string nameField;

    private string nodenameField;

    private string typeField;

    private decimal valueField;

    private bool valueFieldSpecified;

    /// <remarks/>
    [XmlAttribute()]
    public string name
    {
        get
        {
            return nameField;
        }
        set
        {
            nameField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string nodename
    {
        get
        {
            return nodenameField;
        }
        set
        {
            nodenameField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string type
    {
        get
        {
            return typeField;
        }
        set
        {
            typeField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public decimal value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }

    /// <remarks/>
    [XmlIgnore()]
    public bool valueSpecified
    {
        get
        {
            return valueFieldSpecified;
        }
        set
        {
            valueFieldSpecified = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
public partial class materialxNormalmap
{

    private materialxNormalmapInput[] inputField;

    private string nameField;

    private decimal xposField;

    private string typeField;

    private decimal yposField;

    /// <remarks/>
    [XmlElement("input")]
    public materialxNormalmapInput[] input
    {
        get
        {
            return inputField;
        }
        set
        {
            inputField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string name
    {
        get
        {
            return nameField;
        }
        set
        {
            nameField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public decimal xpos
    {
        get
        {
            return xposField;
        }
        set
        {
            xposField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string type
    {
        get
        {
            return typeField;
        }
        set
        {
            typeField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public decimal ypos
    {
        get
        {
            return yposField;
        }
        set
        {
            yposField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
public partial class materialxNormalmapInput
{

    private string nameField;

    private string nodenameField;

    private string typeField;

    private decimal valueField;

    private bool valueFieldSpecified;

    /// <remarks/>
    [XmlAttribute()]
    public string name
    {
        get
        {
            return nameField;
        }
        set
        {
            nameField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string nodename
    {
        get
        {
            return nodenameField;
        }
        set
        {
            nodenameField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string type
    {
        get
        {
            return typeField;
        }
        set
        {
            typeField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public decimal value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }

    /// <remarks/>
    [XmlIgnore()]
    public bool valueSpecified
    {
        get
        {
            return valueFieldSpecified;
        }
        set
        {
            valueFieldSpecified = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
public partial class materialxStandard_surface
{

    private materialxStandard_surfaceInput[] inputField;

    private string nameField;

    private decimal xposField;

    private string typeField;

    private decimal yposField;

    /// <remarks/>
    [XmlElement("input")]
    public materialxStandard_surfaceInput[] input
    {
        get
        {
            return inputField;
        }
        set
        {
            inputField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string name
    {
        get
        {
            return nameField;
        }
        set
        {
            nameField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public decimal xpos
    {
        get
        {
            return xposField;
        }
        set
        {
            xposField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string type
    {
        get
        {
            return typeField;
        }
        set
        {
            typeField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public decimal ypos
    {
        get
        {
            return yposField;
        }
        set
        {
            yposField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
public partial class materialxStandard_surfaceInput
{

    private string nameField;

    private string typeField;

    private string valueField;

    private string nodenameField;

    /// <remarks/>
    [XmlAttribute()]
    public string name
    {
        get
        {
            return nameField;
        }
        set
        {
            nameField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string type
    {
        get
        {
            return typeField;
        }
        set
        {
            typeField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string nodename
    {
        get
        {
            return nodenameField;
        }
        set
        {
            nodenameField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
public partial class materialxSurfacematerial
{

    private materialxSurfacematerialInput[] inputField;

    private string nameField;

    private decimal xposField;

    private string typeField;

    private decimal yposField;

    /// <remarks/>
    [XmlElement("input")]
    public materialxSurfacematerialInput[] input
    {
        get
        {
            return inputField;
        }
        set
        {
            inputField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string name
    {
        get
        {
            return nameField;
        }
        set
        {
            nameField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public decimal xpos
    {
        get
        {
            return xposField;
        }
        set
        {
            xposField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string type
    {
        get
        {
            return typeField;
        }
        set
        {
            typeField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public decimal ypos
    {
        get
        {
            return yposField;
        }
        set
        {
            yposField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
public partial class materialxSurfacematerialInput
{

    private string nameField;

    private string nodenameField;

    private string typeField;

    /// <remarks/>
    [XmlAttribute()]
    public string name
    {
        get
        {
            return nameField;
        }
        set
        {
            nameField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string nodename
    {
        get
        {
            return nodenameField;
        }
        set
        {
            nodenameField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string type
    {
        get
        {
            return typeField;
        }
        set
        {
            typeField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
public partial class materialxTiledimage
{

    private materialxTiledimageInput[] inputField;

    private string nameField;

    private decimal xposField;

    private string typeField;

    private decimal yposField;

    /// <remarks/>
    [XmlElement("input")]
    public materialxTiledimageInput[] input
    {
        get
        {
            return inputField;
        }
        set
        {
            inputField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string name
    {
        get
        {
            return nameField;
        }
        set
        {
            nameField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public decimal xpos
    {
        get
        {
            return xposField;
        }
        set
        {
            xposField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string type
    {
        get
        {
            return typeField;
        }
        set
        {
            typeField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public decimal ypos
    {
        get
        {
            return yposField;
        }
        set
        {
            yposField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
public partial class materialxTiledimageInput
{

    private string nameField;

    private string typeField;

    private string colorspaceField;

    private string valueField;

    /// <remarks/>
    [XmlAttribute()]
    public string name
    {
        get
        {
            return nameField;
        }
        set
        {
            nameField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string type
    {
        get
        {
            return typeField;
        }
        set
        {
            typeField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string colorspace
    {
        get
        {
            return colorspaceField;
        }
        set
        {
            colorspaceField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

