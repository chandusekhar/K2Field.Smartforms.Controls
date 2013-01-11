﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SourceCode.Forms.Controls.Web.Shared;
using K2Field.Smartforms.Controls.InternalControls;
using System.Configuration;

namespace K2Field.Smartforms.Controls.AutocompleteTextbox
{
    [InstallHelpers.RegisterDataType(SourceCode.Forms.Management.ControlDataType.Text)]
    [InstallHelpers.RegisterControlType("Autocomplete Text Box", 
        PropertyXMLResourceName = "K2Field.Smartforms.Controls.AutocompleteTextbox.AutocompleteTextboxProperties.xml",
        SetValueMethod = "UtilitiesBehaviour.setListValue", GetValueMethod = "UtilitiesBehaviour.getValue", 
        SetItemsMethod="UtilitiesBehaviour.setListItems", 
        GetPropertyMethod = "UtilitiesBehaviour.getControlProperty", SetPropertyMethod = "UtilitiesBehaviour.setControlPropertyOrStyle", 
        Category = SourceCode.Forms.Management.ControlTypeCategory.Listing)]
    public class AutocompleteTextbox : BaseControl
    {
        #region Private Variables
        private bool _isVisible = true;
        private string _dataSourceType = "";
        private string _fixedListItems = "";
        private string _displaytemplate = null;
        private string _valueproperty = null;
        private string _associationSO = null;
        private string _associationMethod = "";
        #endregion Private Variables

        #region Constructor
        public AutocompleteTextbox()
        {
            Name = "AutocompleteTextbox";
            FriendlyName = "Autocomplete Text Box";
            //this.CodePaths.Add("AutoCompleteBehavior", "K2Field.Smartforms.Controls.AutocompleteTextbox.AutocompleteTextbox.js");
        }
        #endregion Constructor



        #region Properties
        public bool IsVisible
        {
            get { return this._isVisible; }
            set { this._isVisible = value; }
        }

        public string DataSourceType
        {
            get { return this._dataSourceType; }
            set { this._dataSourceType = value; }
        }

        public string FixedListItems
        {
            get { return this._fixedListItems; }
            set { this._fixedListItems = value; }
        }

        public string DisplayTemplate
        {
            get { return this._displaytemplate; }
            set { this._displaytemplate = value; }
        }

        public string ValueProperty
        {
            get { return this._valueproperty; }
            set { this._valueproperty = value; }
        }

        public string AssociationSO
        {
            get { return this._associationSO; }
            set { this._associationSO = value; }
        }

        public string AssociationMethod
        {
            get { return this._associationMethod; }
            set { this._associationMethod = value; }
        }
        #endregion Properties


        private InternalTextbox CreateTextBox()
        {
            InternalTextbox tb = new InternalTextbox();
            tb.ID = this.ControlID + "_textbox";
            tb.TextMode = System.Web.UI.WebControls.TextBoxMode.SingleLine;
            tb.CssClass = "autocompletetextbox";
            tb.Visible = this.IsVisible;
            tb.Enabled = this.IsEnabled;
            tb.ReadOnly = this.IsReadOnly;
            return tb;
        }

        protected override void CreateChildControls()
        {
            this.Controls.Add(CreateTextBox());
            AutocompleteTextboxExtender ext = new AutocompleteTextboxExtender();
            ext.TargetControlID = this.ControlID + "_textbox";
            ext.ControlID = this.ControlID;

            ext.DataSourceType = DataSourceType;
            ext.FixedListItems = FixedListItems;
            ext.DisplayTemplate = DisplayTemplate;
            ext.ValueProperty = ValueProperty;
            ext.AssociationSO = AssociationSO;
            ext.AssociationMethod = AssociationMethod;
            this.Controls.Add(ext);

            base.CreateChildControls();
        }


        /// <summary>
        /// See IframeControls comment.
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderControl(System.Web.UI.HtmlTextWriter writer)
        {
            base.RenderControl(writer);
            if (State == ControlState.Designtime || State == ControlState.Preview)
            {
                InternalTextbox tb = CreateTextBox();
                if (!tb.Visible)
                {
                    InternalLiteral l = new InternalLiteral();
                    l.Text = "Invisible autocomplete textbox";
                    l.RenderControl(writer);
                }
                else
                {
                    tb.RenderControl(writer);
                }
            }
        }
    }
}
