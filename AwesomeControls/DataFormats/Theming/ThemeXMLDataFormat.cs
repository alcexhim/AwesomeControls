using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Markup;

using AwesomeControls.ObjectModels.Theming;
using AwesomeControls.ObjectModels.Theming.RenderingActions;

namespace AwesomeControls.DataFormats.Theming
{
	public class ThemeXMLDataFormat : XMLDataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
			}
			return _dfr;
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new MarkupObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			MarkupObjectModel mom = (objectModels.Pop() as MarkupObjectModel);
			ThemeObjectModel themes = (objectModels.Pop() as ThemeObjectModel);

			MarkupTagElement tagThemes = (mom.FindElement("AwesomeControls", "Theming", "Themes") as MarkupTagElement);
			if (tagThemes == null) throw new InvalidDataFormatException("File does not contain the tag path 'AwesomeControls/Theming/Themes'");

			foreach (MarkupElement elTheme in tagThemes.Elements)
			{
				MarkupTagElement tagTheme = (elTheme as MarkupTagElement);
				if (tagTheme == null) continue;

				MarkupAttribute attThemeID = tagTheme.Attributes["ID"];
				if (attThemeID == null) continue;

				Theme theme = new Theme();
				theme.ID = new Guid(attThemeID.Value);

				MarkupAttribute attInheritsThemeID = tagTheme.Attributes["InheritsThemeID"];
				if (attInheritsThemeID != null) theme.InheritsThemeID = new Guid(attInheritsThemeID.Value);

				MarkupTagElement tagInformation = (tagTheme.Elements["Information"] as MarkupTagElement);
				if (tagInformation != null)
				{
					MarkupTagElement tagInformationTitle = (tagInformation.Elements["Title"] as MarkupTagElement);
					if (tagInformationTitle != null) theme.Title = tagInformationTitle.Value;
				}

				MarkupTagElement tagColors = (tagTheme.Elements["Colors"] as MarkupTagElement);
				if (tagColors != null)
				{
					foreach (MarkupElement elColor in tagColors.Elements)
					{
						MarkupTagElement tagColor = (elColor as MarkupTagElement);
						if (tagColor == null) continue;
						if (tagColor.FullName != "Color") continue;

						MarkupAttribute attColorID = tagColor.Attributes["ID"];
						MarkupAttribute attColorName = tagColor.Attributes["Name"];

						if (attColorID == null && attColorName == null) continue;

						MarkupAttribute attColorValue = tagColor.Attributes["Value"];
						if (attColorValue == null) continue;

						ThemeColor color = new ThemeColor();
						if (attColorID != null) color.ID = new Guid(attColorID.Value);
						if (attColorName != null) color.Name = attColorName.Value;
						if (attColorValue != null) color.Value = attColorValue.Value;

						theme.Colors.Add(color);
					}
				}

				MarkupTagElement tagComponents = (tagTheme.Elements["Components"] as MarkupTagElement);
				if (tagComponents != null)
				{
					foreach (MarkupElement elComponent in tagComponents.Elements)
					{
						MarkupTagElement tagComponent = (elComponent as MarkupTagElement);
						if (tagComponent == null) continue;
						if (tagComponent.FullName != "Component") continue;

						MarkupAttribute attComponentID = tagComponent.Attributes["ID"];
						if (attComponentID == null) continue;

						ThemeComponent component = new ThemeComponent();
						component.ID = new Guid(attComponentID.Value);

						MarkupAttribute attInheritsComponentID = tagComponent.Attributes["InheritsComponentID"];
						if (attInheritsComponentID != null) component.InheritsComponentID = new Guid(attInheritsComponentID.Value);

						MarkupTagElement tagComponentStates = (tagComponent.Elements["States"] as MarkupTagElement);
						if (tagComponentStates != null)
						{
							// if States is specified, only apply to specific states
							foreach (MarkupElement elState in tagComponentStates.Elements)
							{
								MarkupTagElement tagState = (elState as MarkupTagElement);
								if (tagState == null) continue;
								if (tagState.FullName != "State") continue;

								MarkupAttribute attStateID = tagState.Attributes["ID"];
								if (attStateID == null) continue;

								ThemeComponentState state = new ThemeComponentState();
								state.ID = new Guid(attStateID.Value);

								MarkupAttribute attStateName = tagState.Attributes["Name"];
								if (attStateName != null) state.Name = attStateName.Value;

								component.States.Add(state);
							}
						}

						MarkupTagElement tagRenderings = (tagComponent.Elements["Renderings"] as MarkupTagElement);
						if (tagRenderings != null)
						{
							foreach (MarkupElement elRendering in tagRenderings.Elements)
							{
								MarkupTagElement tagRendering = (elRendering as MarkupTagElement);
								if (tagRendering == null) continue;
								if (tagRendering.FullName != "Rendering") continue;

								MarkupTagElement tagRenderingActions = (tagRendering.Elements["Actions"] as MarkupTagElement);
								if (tagRenderingActions == null) continue;

								Rendering rendering = new Rendering();
								foreach (MarkupElement elRenderingAction in tagRenderingActions.Elements)
								{
									MarkupTagElement tagRenderingAction = (elRenderingAction as MarkupTagElement);
									if (tagRenderingAction == null) continue;

									switch (tagRenderingAction.FullName)
									{
										case "Rectangle":
											{
												MarkupAttribute attX = tagRenderingAction.Attributes["X"];
												MarkupAttribute attY = tagRenderingAction.Attributes["Y"];
												MarkupAttribute attWidth = tagRenderingAction.Attributes["Width"];
												MarkupAttribute attHeight = tagRenderingAction.Attributes["Height"];

												RectangleRenderingAction item = new RectangleRenderingAction();
												item.X = RenderingExpression.Parse(attX.Value);
												item.Y = RenderingExpression.Parse(attY.Value);
												item.Width = RenderingExpression.Parse(attWidth.Value);
												item.Height = RenderingExpression.Parse(attHeight.Value);

												MarkupTagElement tagOutline = (tagRenderingAction.Elements["Outline"] as MarkupTagElement);
												if (tagOutline != null)
												{
													MarkupAttribute attOutlineType = tagOutline.Attributes["Type"];
													if (attOutlineType != null)
													{
														switch (attOutlineType.Value.ToLower())
														{
															case "none":
															{
																break;
															}
															case "solid":
															{
																MarkupAttribute attColor = tagOutline.Attributes["Color"];
																if (attColor != null)
																{
																	item.Outline = new Outline();
																	item.Outline.Color = attColor.Value;

																	MarkupAttribute attOutlineWidth = tagOutline.Attributes["Width"];
																	if (attOutlineWidth != null)
																	{
																		item.Outline.Width = Single.Parse(attOutlineWidth.Value);
																	}
																}
																break;
															}
														}
													}
												}

												MarkupTagElement tagFill = (tagRenderingAction.Elements["Fill"] as MarkupTagElement);
												if (tagFill != null)
												{
													MarkupAttribute attFillType = tagFill.Attributes["Type"];
													if (attFillType != null)
													{
														switch (attFillType.Value.ToLower())
														{
															case "none":
															{
																break;
															}
															case "solid":
															{
																MarkupAttribute attFillColor = tagFill.Attributes["Color"];
																if (attFillColor != null)
																{
																	item.Fill = new SolidFill(attFillColor.Value);
																}
																break;
															}
														}
													}
												}

												rendering.Actions.Add(item);
												break;
											}
									}
								}

								MarkupTagElement tagRenderingStates = (tagRendering.Elements["States"] as MarkupTagElement);
								if (tagRenderingStates != null)
								{
									// if States is specified, only apply to specific states
									foreach (MarkupElement elState in tagRenderingStates.Elements)
									{
										MarkupTagElement tagState = (elState as MarkupTagElement);
										if (tagState == null) continue;
										if (tagState.FullName != "State") continue;

										MarkupAttribute attStateID = tagState.Attributes["ID"];
										if (attStateID == null) continue;

										ThemeComponentStateReference state = new ThemeComponentStateReference();
										state.StateID = new Guid(attStateID.Value);
										rendering.States.Add(state);
									}
								}

								component.Renderings.Add(rendering);
							}
						}

						theme.Components.Add(component);
					}
				}
				themes.Themes.Add(theme);
			}
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			ThemeObjectModel theme = (objectModels.Pop() as ThemeObjectModel);
			MarkupObjectModel mom = new MarkupObjectModel();



			objectModels.Push(mom);
		}
	}
}
