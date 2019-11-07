<%@ Page Language="C#" AutoEventWireup="true" CodeFile="upload.aspx.cs" Inherits="Content_upload" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxUploadControl" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
    // <![CDATA[
        function Uploader_OnUploadStart() {
            btUpload.SetEnabled(false);
        }
        function Uploader_OnFileUploadComplete(args) {
            var imgSrc = aspxPreviewImgSrc;

            if (args.isValid) {
                
                var date = new Date();
                
                imgSrc =  args.callbackData  + "?dx=" + date.getTime();
            }

            getPreviewImageElement().src = imgSrc;
        }
        function Uploader_OnFilesUploadComplete(args) {
            UpdateUploadButton();
        }
        function UpdateUploadButton() {
            btUpload.SetEnabled(upFoto.GetText(0) != "");
        }
        function getPreviewImageElement() {

            return document.getElementById("previewImage");
        }

        function FazerUp() {
            upFoto.Upload();
        }
    // ]]> 
    </script>
    <style type="text/css">
        .style1
        {
            width: 46%;
        }
.dxpcControl_DevEx
{
	font: 11px Verdana;
	color: #201f35;
	background-color: White;
	border: 1px solid #9da0aa;
	width: 200px;
}
#mainContainer td.imagePreviewCell 
{
            border: solid 2px gray;
            width: 70px;
            height: 115px;
            /*if IE*/
            height:expression("110px");
            text-align: center;
}
.dxpcHeader_DevEx
{
	color: #5d5c6d;
	border-bottom: 1px solid #a8aab4;
}
.dxpcCloseButton_DevEx
{
	padding: 3px 3px 3px 1px;
}

.dxWeb_pcCloseButton_DevEx {
    background-position: 0px -40px;
    width: 29px;
    height: 15px;
}

.dxWeb_rpHeaderTopLeftCorner_DevEx,
.dxWeb_rpHeaderTopRightCorner_DevEx,
.dxWeb_rpBottomLeftCorner_DevEx,
.dxWeb_rpBottomRightCorner_DevEx,
.dxWeb_rpTopLeftCorner_DevEx,
.dxWeb_rpTopRightCorner_DevEx,
.dxWeb_rpGroupBoxBottomLeftCorner_DevEx,
.dxWeb_rpGroupBoxBottomRightCorner_DevEx,
.dxWeb_rpGroupBoxTopLeftCorner_DevEx,
.dxWeb_rpGroupBoxTopRightCorner_DevEx,
.dxWeb_mHorizontalPopOut_DevEx,
.dxWeb_mVerticalPopOut_DevEx,
.dxWeb_mVerticalPopOutRtl_DevEx,
.dxWeb_mSubMenuItem_DevEx,
.dxWeb_mSubMenuItemChecked_DevEx,
.dxWeb_mScrollUp_DevEx,
.dxWeb_mScrollDown_DevEx,
.dxWeb_tcScrollLeft_DevEx,
.dxWeb_tcScrollRight_DevEx,
.dxWeb_tcScrollLeftHover_DevEx,
.dxWeb_tcScrollRightHover_DevEx,
.dxWeb_tcScrollLeftPressed_DevEx,
.dxWeb_tcScrollRightPressed_DevEx,
.dxWeb_tcScrollLeftDisabled_DevEx,
.dxWeb_tcScrollRightDisabled_DevEx,
.dxWeb_nbCollapse_DevEx,
.dxWeb_nbExpand_DevEx,
.dxWeb_splVSeparator_DevEx,
.dxWeb_splVSeparatorHover_DevEx,
.dxWeb_splHSeparator_DevEx,
.dxWeb_splHSeparatorHover_DevEx,
.dxWeb_splVCollapseBackwardButton_DevEx,
.dxWeb_splVCollapseBackwardButtonHover_DevEx,
.dxWeb_splHCollapseBackwardButton_DevEx,
.dxWeb_splHCollapseBackwardButtonHover_DevEx,
.dxWeb_splVCollapseForwardButton_DevEx,
.dxWeb_splVCollapseForwardButtonHover_DevEx,
.dxWeb_splHCollapseForwardButton_DevEx,
.dxWeb_splHCollapseForwardButtonHover_DevEx,
.dxWeb_pcCloseButton_DevEx,
.dxWeb_pcSizeGrip_DevEx,
.dxWeb_pcSizeGripRtl_DevEx,
.dxWeb_pAll_DevEx,
.dxWeb_pAllDisabled_DevEx,
.dxWeb_pPrev_DevEx,
.dxWeb_pPrevDisabled_DevEx,
.dxWeb_pNext_DevEx,
.dxWeb_pNextDisabled_DevEx,
.dxWeb_pLast_DevEx,
.dxWeb_pLastDisabled_DevEx,
.dxWeb_pFirst_DevEx,
.dxWeb_pFirstDisabled_DevEx,
.dxWeb_tvColBtn_DevEx,
.dxWeb_tvColBtnRtl_DevEx,
.dxWeb_tvExpBtn_DevEx,
.dxWeb_tvExpBtnRtl_DevEx,
.dxWeb_ncBackToTop_DevEx,
.dxWeb_smBullet_DevEx,
.dxWeb_tiBackToTop_DevEx,
.dxWeb_fmFolder_DevEx,
.dxWeb_fmFolderLocked_DevEx,
.dxWeb_fmCreateButton_DevEx,
.dxWeb_fmMoveButton_DevEx,
.dxWeb_fmRenameButton_DevEx,
.dxWeb_fmDeleteButton_DevEx,
.dxWeb_fmRefreshButton_DevEx,
.dxWeb_fmDwnlButton_DevEx,
.dxWeb_fmCreateButtonDisabled_DevEx,
.dxWeb_fmMoveButtonDisabled_DevEx,
.dxWeb_fmRenameButtonDisabled_DevEx,
.dxWeb_fmDeleteButtonDisabled_DevEx,
.dxWeb_fmRefreshButtonDisabled_DevEx,
.dxWeb_fmDwnlButtonDisabled_DevEx,
.dxWeb_ucClearButton_DevEx,
.dxWeb_ucClearButtonDisabled_DevEx { 
    background-repeat: no-repeat;
    background-color: transparent;
    display:block;
}
.dxpcContent_DevEx
{
	white-space: normal;
	vertical-align: top;
}
.dxpcContentPaddings_DevEx 
{
	padding: 9px 12px 10px;
}
.dxucControl_DevEx,
.dxucEditArea_DevEx
{
	font: 11px Verdana;
}
.dxucTextBox_DevEx
{
	background-color: white;
	border-top: 1px solid #9DA0AA;
	border-right: 1px solid #C2C4CB;
	border-bottom: 1px solid #D9DAE0;
	border-left: 1px solid #C2C4CB;
	padding: 1px 2px;
}
.dxucTextBox_DevEx .dxucEditArea_DevEx
{
	margin: 0px;
	background-color: white;
}
.dxpcControl_DevEx a
{
	color: #1b3f91;
}

a:link, a:visited
{
    color: #666666;
}

.dxWeb_ucClearButton_DevEx {
    background-position: -100px -123px;
    width: 16px;
    height: 16px;
}
.dxucBrowseButton_DevEx
{
	padding: 4px 15px;
	border: 1px solid #A9ACB5;
}
.dxucBrowseButton_DevEx,
.dxucBrowseButton_DevEx a
{
	color: #201F35;
	cursor: pointer;
	white-space: nowrap;
	text-decoration: none;
}
.dxucControl_DevEx .dxucBrowseButton_DevEx a
{
	color: #201F35;
}
.dxucProgressBar_DevEx
{
	border: 1px solid #b9bac3;
}
.dxucProgressBarIndicator_DevEx
{
}
.dxucButton_DevEx,
.dxucButton_DevEx a
{
	color: #1B3F91;
	text-decoration: none;
}
.dxbButton_DevEx
{
	color: #201f35;
	font: normal 11px Verdana;
	vertical-align: middle;
	border: 1px solid #a9acb5;
	padding: 1px;
	cursor: pointer;
}
.dxbButton_DevEx div.dxb
{
	padding: 3px 15px;
	border-width: 0px;
}
        .style2
        {
            width: 357px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table class="style1">
        <tr>
            <td class="style2">
    <dx:ASPxUploadControl runat="server" ClientInstanceName="upFoto" ShowProgressPanel="True" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="330px" 
                    CssPostfix="DevEx" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" ID="upFoto" 
                    OnFileUploadComplete="upFoto_FileUploadComplete">
<ValidationSettings MultiSelectionErrorText="Attention! The following {0} files are invalid because they exceed the allowed file size ({1}) or their extensions are not allowed. These files have been removed from selection, so they will not be uploaded. {2}" 
            AllowedFileExtensions=".jpg, .jpeg, .png, .gif" 
            GeneralErrorText="Exten&#231;&#227;o de arquivo inv&#225;lida" 
            MaxFileSize="1048576" 
            MaxFileSizeErrorText="Arquivo muito grande, tente novamente"></ValidationSettings>
<ClientSideEvents FileUploadComplete="function(s, e) {Uploader_OnFileUploadComplete(e);}"
 FileUploadStart="function(s, e){Uploader_OnUploadStart();}" 
 TextChanged="function(s, e) {UpdateUploadButton();}" FilesUploadComplete="function(s, e) {
	UpdateUploadButton();
}"></ClientSideEvents>

<BrowseButton Text="Procurar"></BrowseButton>

<RemoveButton Text="Remover"></RemoveButton>

<CancelButton Text="Cancelar"></CancelButton>
</dx:ASPxUploadControl>

            </td>
            <td align="left" class="imagePreviewCell">
                <img src="../Content/Semfoto.jpg" id="previewImage" alt="" width="245" height="184" />
            </td>
        </tr>
        <tr>
            <td class="style2">

    <dx:ASPxButton runat="server" ClientInstanceName="btUpload" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Subir Foto" 
                    CssPostfix="DevEx" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                    Width="100px" ID="btUpload" AutoPostBack="False" ClientEnabled="False">
<ClientSideEvents Click="function(s, e)
{
FazerUp();	
}"></ClientSideEvents>
</dx:ASPxButton>

                        </td>
            <td align="center" class="imagePreviewCell">
                <dx:ASPxImage ID="ASPxImage1" runat="server">
                </dx:ASPxImage>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
    // <![CDATA[
            var aspxPreviewImgSrc = getPreviewImageElement().src;
    // ]]>
    </script>
    </form>
</body>
</html>
