   M S B A M L   `   `   `  ���� K  GMonitor.TaskView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Monitor.TaskView.GlobalsMonitor.TaskView.Globals  E'clr-namespace:Monitor.TaskView.ResourceMonitor.TaskView.Resource  =#clr-namespace:Monitor.TaskView.ViewMonitor.TaskView.View  *    $Monitor.TaskView.View.PasswordWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     :globals&clr-namespace:Monitor.TaskView.Globals     <controls'clr-namespace:Monitor.TaskView.Resource     5local#clr-namespace:Monitor.TaskView.View     -   $	��200��$	��300��  � 
ResizeMode$  NoResize=�$	��300��$	��229�� � WindowStartupLocation$ CenterScreen=� � Icon$7 1/Monitor.TaskView;component/Resource/TaskView.ico��
���   �.��� ����  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ #eeeeee�� � Offset$ 0Q�� $ #c3c3c3��$ 1Q�� $ #eeeeee��$ 0.866Q� � RowDefinitions �� $��50 ��� .+�� -   $��50��$	��Top=����	#FF40A6D1 J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect  	   
BlurRadius$	 10Q�
   ShadowDepth$
 3Q�   	Direction$	 270Q�   Opacity$	 0.2Q� � ColumnDefinitions �� $
��Auto ��� �� $
��Auto ��� $��60 �.+�0   *Monitor.TaskView.Resource.HeaderDecoration  ]� $��2��$��0��$
��Left=�$��50����.  .+��� -    ��Name$ AVARTA��$��25��$��25�� K� ZIndex$ 1��$
��Left=�$��	14,0,0,10q�$��/Resource/net-on.png��$��Uniform=� J� RenderTransformOrigin$ 0.396,1.005&�$��Bottom=��� $�� Password of Server��$��Center=�$��Center=�
�������$��SemiBold"�$��21(� J� IsManipulationEnabled . $��0,5,5,5q�$	��0,6q��� $��2��$��2��$��Center=� �� Orientation$ 
Horizontal=�$��0,0,10,0q�.+��� -   $��40��$��40��$��20,0,0,0q�$	����   CloseMinMaxButtonStyle#���   
������ 
������ 
�������$.��(pack://application:,,,/Font/#FontAwesome)�$��20(�$��Bottom=�|�.  � $��0��$��1��$��Stretch=�$��Stretch=�$��	0,30,0,-4q� �� $��* ��� $��212.714 ��� $��61.286 � �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* �.+�]� $��1��$��2��$��1��$��0,0,20,5q���.  .+�G� -    G�Name$ PasswordTextBox��$	��Top=�$��35��$��Center=� " RoundedGlowingPasswordBoxStyle#���   � ToolTip$ Password��$��2��$ 0.524,-0.843&�{��� *IsAvailable+�� $
��Left=�$
��10px(�
�������  LinkLabelStyle#���  |�. $��5��.���� $@��:You will need to ask your administrator for the password. ��  LinkLabelTextBlockStyle#���  $	��254��$��9(��� $
��Left=�$
��10px(�
�������$��-110 20 0 0q�#���  |�. $��5���� $��1��$��2��$ Vertical=�$��0 0 20 0q�$��2��.+��� $ 
Horizontal=�$��0 0 0 0q�{��� *IsAvailable+.+��� -    ��Name$ RememberCheckBox��$��Remember me��$��Center=�$��0 0 50 0q�
���iii�  RoundedCheckBoxStyle#���  $��4�� d� 	IsChecked$
 TrueY��� -    ��Name$ LoginButton��$��LOG IN��  RoundedButtonStyle#���  $
��25 5q�$��SemiBold"�
����������	#FF40A6D1$��14(�$��3����Z� 