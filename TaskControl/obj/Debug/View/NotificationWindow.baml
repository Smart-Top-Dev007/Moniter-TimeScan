   M S B A M L   `   `   `  ����N  JMonitor.TaskControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Monitor.TaskControl.ViewMonitor.TaskControl.View  5   	   1    +Monitor.TaskControl.View.NotificationWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:Monitor.TaskControl.View     5   	     � Title$  NotificationWindow��5   
   $	��350��6%    � SizeToContent$ Height=�61    � WindowStyle$
 None=�6H    � AllowsTransparency . 
������ $	��125��6�   .��5	      � -   5        ��5
   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ #eeeeee��6    � Offset$ 0Q�6/   � 5      $ #c3c3c3��6   $ 1Q�6/   � 5      $ #eeeeee��6   $ 0.866Q�6/   5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   � $	��Top=�6   
���   �$��0��6;   	 J� Effect	 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      
   
BlurRadius$
 10Q�6$      ShadowDepth$ 3Q�64      	Direction$	 270Q�6D      Opacity$	 0.2Q�6T    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      �� 5      $
��Auto �6#   .+�5       ]� $��2��6   $��0��6,   $
��Left=�6<   ��.  $	��349��6m   .+�5!      �� -   5         ��Name$ AVARTA��5!      $��18��6(   $��18��64    K� ZIndex$ 1��6?   $
��Left=�6P   $��14,0,0,4q�6k   $��/Resource/net-on.png��6}   $��Uniform=�6�    J� RenderTransformOrigin$ 0.396,1.005&�6�   $��Bottom=�6�   �� 5#      $�� Alarm��6   $��Center=�6)   $��Bottom=�6K   
�������$��SemiBold"�5$      $��14(�6.    J� IsManipulationEnabled . $��0,5,5,5q�6Y   $��7,-1,0,0q�6k   $��31��6}   $	��100��6�   �� 5%      -   5         ��Name$ 
alarm_date��5%      $��
2017/12/28��6,   $��Center=�6A   $��Bottom=�6c   
�������$��SemiBold"�5&      $��12(�6+    . $��0,5,5,5q�6V   $��7,-1,0,0q�6h   $��31��6z   $	��168��6�   �� 5'      -   5        $��35��5'      $	����6$      CloseMinMaxButtonStyle#���   60   
������ 
������ 
�������$2��,pack://application:,,,/Resource/#FontAwesome)�5)      $��20(�6S   $��Bottom=�5*      |�.  6W   5+      5,      � 5.   
   $��0��6   $��Stretch=�6   $��Stretch=�6=   $��0,0,0,0q�6Y   $��1��6j   $ 0.5,0.5&�6w    50      �� 51      $��1* �6#   �� 52      $��1* �6#    54      �� 55      $
��Auto �6    �� 56      $
��Auto �6    .+�59      �� -   5        $ lbl_message��59      $��Label��6)   $��2��69   $
��Left=�6M   $��Center=�6i   $��Center=�6�   $	��300��6�   $ 0.5,0.5&�6�   $��Sitka Small)�6�   $��30��6�   $��	24,0,0,27q�6�   ���	#FF5F5F5F$��1��68  ��6  Z� 68  �� 5:      -   5        $ ClientIP��5:      $��0��6&   $��0��63   ���	#FF5F5F5F$��Label��6Z   $
��Left=�6j   $��Center=�6�   $��Center=�6�   $	��164��6�   $ 0.5,0.5&�6�   $��24��6�   $��10,6,0,5q�6�   �� 5;      -   5        $ 
ClientName��5;      $��0��6)   $��1��66   ���	#FF5F5F5F$��Label��6]   $
��Left=�6m   $��Center=�6�   $��Center=�6�   $	��164��6�   $ 0.5,0.5&�6�   $��Sitka Small)�6�   $��23��6  $��10,6,0,6q�6  5=      �� 5>      -   5        $ lbl_information��5>      $��Label��6-   $��2��6=   $
��Left=�6Q   $��Center=�6m   $��Center=�6�   $	��300��6�   $ 0.5,0.5&�6�   $��Sitka Small)�6�   $��26��6�   $��1��6�   $��	24,30,0,1q�6
  ���	#FF5F5F5F$
��Bold"�64  5?       � Triggers 5@   
   :� 5A       :� RoutedEvent$ FrameworkElement.Loaded��6   .[�5B      �� -	   5         ��Name$ StoryboardLoad��5B   "   .��5C      �� .��5D      W�  �� TargetProperty$ (UIElement.Opacity)�6*    W� From$	 0.0X�6Z   	 W� To$	 1.0X�6e    g� Duration$ 0:0:2D�6n   W� 5E      -
   5        $ (UIElement.Opacity)�5E   *   $	 1.0X�6Z   $	 0.0X�6e   $ 0:0:8D�6n    g� 	BeginTime$ 0:0:5X�6   6�   5F      5G      5H      :� 5J      $ Mouse.MouseEnter��6   [�5K      �� 5L       �� BeginStoryboardName$ StoryboardLoad��6'   �� 5M      $ StoryboardFade��6'   5O      :� 5Q      $ Mouse.MouseLeave��6   .[�5R      �� -   5        $ StoryboardFade��5R   "   .��5S      �� .��5T      W� -   5        $ (UIElement.Opacity)�5T   *   $	 1.0X�6Z   $	 0.0X�6e   $ 0:0:8D�6n   $ 0:0:2X�6   6�   5U      5V      5W      5Z      5[      