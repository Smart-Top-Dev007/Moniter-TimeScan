   M S B A M L   `   `   `  ���� K  GMonitor.TaskView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Monitor.TaskView.GlobalsMonitor.TaskView.Globals  E'clr-namespace:Monitor.TaskView.ResourceMonitor.TaskView.Resource  =#clr-namespace:Monitor.TaskView.ViewMonitor.TaskView.View  +    %Monitor.TaskView.View.CustomMsgWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     :globals&clr-namespace:Monitor.TaskView.Globals     <controls'clr-namespace:Monitor.TaskView.Resource     5local#clr-namespace:Monitor.TaskView.View     -   $	��200��$	��300��  � 
ResizeMode$  NoResize=�$	��350��$	��210�� � WindowStartupLocation$ CenterScreen=�
���   �.��� ����  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ #eeeeee�� � Offset$ 0Q�� $ #c3c3c3��$ 1Q�� $ #eeeeee��$ 0.866Q� � RowDefinitions �� $��0 ��� .+�� -   $��30��$	��Top=����	#FF40A6D1$��1,0,0,0q�$��2�� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 10Q�	   ShadowDepth$	 3Q�
   	Direction$	
 270Q�   Opacity$	 0.2Q� � ColumnDefinitions �� $
��Auto ��� �� $
��Auto �.+�0   *Monitor.TaskView.Resource.HeaderDecoration  $��2��$��
-1,0,132,0q�]� $��2��$��0��$
��Left=���.  .+��� $��	 Warning!��$��Center=�$��Bottom=�
�������$��SemiBold"�$��14(� J� IsManipulationEnabled . $��0,5,5,5q�$��	7,-1,0,-3q�$��34��$	��112���� $��1��$��2��$��Center=� �� Orientation$ 
Horizontal=�$��0,0,0,0q�.+��� -   $��30��$��30��$��0,0,0,0q�$	����   CloseMinMaxButtonStyle#���   
������ 
������ 
�������$.��(pack://application:,,,/Font/#FontAwesome)�$��20(�$��Bottom=�|�.  � $��0��$��35��$��Stretch=�$��Stretch=�$��0,0,0,80q�$��1�� �� $��* � �� $
��Auto �$��35��� $��0��$��Stretch=�$��Stretch=�$��	0,63,0,68q�$��1�� �� $��* � �� $
��Auto �$��35��.+��� -    ��Name$ btn_Msg��$��Stretch=�$��0,5,5,0q�$	��0 2q�  RoundedSettingsButtonStyle#���  ���#eeeeee
�������$��0q���Z� .���� $ 
Horizontal=�.+��� $��25,9,9,6q�$��29��$��/Resource/Warning.ico��$
��Fill=�$��29���� -    ��Name$ lbl_Msg��$#��You are missing the anything.��$��0,9,10,6q�$��Center=�$	��Top=�$��Sitka Small)�$��16(�$	��253�� J� RenderTransformOrigin$ 
0.15,0.471&�$��27��� $��0��$��Stretch=�$��Stretch=�$��
0,134,0,10q�$��1�� �� $��* � �� $
��Auto �$��30��.+��� -   $ LoginButton��$��OK��  RoundedButtonStyle#���  $
��25 5q�$��SemiBold"�
����������	#FF40A6D1$��14(�$��3��$	��100��$ 1.174,0.864&�$��30��$��Center=���Z� � $��0��$��Stretch=�$��Stretch=�$��	0,149,0,1q�$��1�� �� $��* � �� $
��Auto �$��30��