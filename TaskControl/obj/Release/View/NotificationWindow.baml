   M S B A M L   `   `   `  ���� N  JMonitor.TaskControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Monitor.TaskControl.ViewMonitor.TaskControl.View  1    +Monitor.TaskControl.View.NotificationWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     8local&clr-namespace:Monitor.TaskControl.View       � Title$  NotificationWindow��$	��350�� � SizeToContent$ Height=� � WindowStyle$
 None=� � AllowsTransparency . 
������ $	��125��.��� -   ����  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ #eeeeee�� � Offset$ 0Q�� $ #c3c3c3��$ 1Q�� $ #eeeeee��$ 0.866Q� � RowDefinitions �� $
��Auto ��� $
��Auto �.+�� $	��Top=�
���   �$��0��	 J� Effect	 3  -System.Windows.Media.Effects.DropShadowEffect  
   
BlurRadius$
 10Q�   ShadowDepth$ 3Q�   	Direction$	 270Q�   Opacity$	 0.2Q� � ColumnDefinitions �� $
��Auto ��� �� $
��Auto �.+�]� $��2��$��0��$
��Left=���.  $	��349��.+��� -    ��Name$ AVARTA��$��18��$��18�� K� ZIndex$ 1��$
��Left=�$��14,0,0,4q�$��/Resource/net-on.png��$��Uniform=� J� RenderTransformOrigin$ 0.396,1.005&�$��Bottom=��� $�� Alarm��$��Center=�$��Bottom=�
�������$��SemiBold"�$��14(� J� IsManipulationEnabled . $��0,5,5,5q�$��7,-1,0,0q�$��31��$	��100���� -    ��Name$ 
alarm_date��$��
2017/12/28��$��Center=�$��Bottom=�
�������$��SemiBold"�$��12(� . $��0,5,5,5q�$��7,-1,0,0q�$��31��$	��168���� -   $��35��$	����   CloseMinMaxButtonStyle#���   
������ 
������ 
�������$2��,pack://application:,,,/Resource/#FontAwesome)�$��20(�$��Bottom=�|�.  � $��0��$��Stretch=�$��Stretch=�$��0,0,0,0q�$��1��$ 0.5,0.5&� �� $��1* ��� $��1* � �� $
��Auto ��� $
��Auto �.+��� -   $ lbl_message��$��Label��$��2��$
��Left=�$��Center=�$��Center=�$	��300��$ 0.5,0.5&�$��Sitka Small)�$��30��$��	24,0,0,27q����	#FF5F5F5F$��1����Z� �� -   $ ClientIP��$��0��$��0�����	#FF5F5F5F$��Label��$
��Left=�$��Center=�$��Center=�$	��164��$ 0.5,0.5&�$��24��$��10,6,0,5q��� -   $ 
ClientName��$��0��$��1�����	#FF5F5F5F$��Label��$
��Left=�$��Center=�$��Center=�$	��164��$ 0.5,0.5&�$��Sitka Small)�$��23��$��10,6,0,6q��� -   $ lbl_information��$��Label��$��2��$
��Left=�$��Center=�$��Center=�$	��300��$ 0.5,0.5&�$��Sitka Small)�$��26��$��1��$��	24,30,0,1q����	#FF5F5F5F$
��Bold"� � Triggers :�  :� RoutedEvent$ FrameworkElement.Loaded��.[��� -	    ��Name$ StoryboardLoad��.���� .��W�  �� TargetProperty$ (UIElement.Opacity)� W� From$	 0.0X�	 W� To$	 1.0X� g� Duration$ 0:0:2D�W� -
   $ (UIElement.Opacity)�$	 1.0X�$	 0.0X�$ 0:0:8D� g� 	BeginTime$ 0:0:5X�:� $ Mouse.MouseEnter��[���  �� BeginStoryboardName$ StoryboardLoad���� $ StoryboardFade��:� $ Mouse.MouseLeave��.[��� -   $ StoryboardFade��.���� .��W� -   $ (UIElement.Opacity)�$	 1.0X�$	 0.0X�$ 0:0:8D�$ 0:0:2X�